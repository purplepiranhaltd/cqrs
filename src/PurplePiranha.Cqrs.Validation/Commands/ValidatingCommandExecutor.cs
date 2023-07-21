using FluentValidation.Results;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;
using System.Reflection;
using System.Runtime.ExceptionServices;
using ValidationFailure = PurplePiranha.Cqrs.Validation.Failures.ValidationFailure;

namespace PurplePiranha.Cqrs.Validation.Commands;

public class ValidatingCommandExecutor : ICommandExecutor
{
    private readonly ICommandExecutor _commandExecutor;
    private readonly IValidatorExecutor _validatorExecutor;

#nullable disable
    private static readonly MethodInfo PerformExecutionTAsyncMethod =
        typeof(ValidatingCommandExecutor)
            .GetMethod(
                nameof(PerformExecutionTAsync),
                BindingFlags.NonPublic | BindingFlags.Instance
            );

    private static readonly MethodInfo PerformValidatationAsyncMethod =
        typeof(ValidatingCommandExecutor)
            .GetMethod(
                nameof(PerformValidatationAsync),
                BindingFlags.NonPublic | BindingFlags.Instance
            );
#nullable enable

    public ValidatingCommandExecutor(
        ICommandExecutor commandExecutor,
        IValidatorExecutor validatorExecutor
        )
    {
        _commandExecutor = commandExecutor;
        _validatorExecutor = validatorExecutor;
    }

    public async Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        return await PerformExecutionAsync<TCommand>(command);
    }

    public async Task<Result<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command)
    {
        return await CallPerformExecuteTAsync<TResult>(command);
    }

    private async Task<Result<TResult>> CallPerformExecuteTAsync<TResult>(ICommand<TResult> command)
    {
        var commandType = command.GetType();
        var resultType = typeof(TResult);

        try
        {
            var method = PerformExecutionTAsyncMethod.MakeGenericMethod(commandType, resultType);

#nullable disable
            return await (Task<Result<TResult>>)method.Invoke(this, new object[] { command });
#nullable enable

        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException is null)
                throw;

            var info = ExceptionDispatchInfo.Capture(ex.InnerException);
            info.Throw();

            // compiler requires assignment - an exception is always thrown so we can never get here
            return default;
        }
    }

    private async Task<ValidationResult> CallPerformValidatationAsync<TCommand>(TCommand command)
    {
        var queryType = command.GetType();

        try
        {
            var method = PerformValidatationAsyncMethod.MakeGenericMethod(queryType);

#nullable disable
            return await (Task<ValidationResult>)method.Invoke(this, new object[] { command });
#nullable enable

        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException is null)
                throw;

            var info = ExceptionDispatchInfo.Capture(ex.InnerException);
            info.Throw();

            // compiler requires assignment - an exception is always thrown so we can never get here
            return default;
        }
    }

    private async Task<Result<TResult>> PerformExecutionTAsync<TCommand, TResult>(TCommand query) where TCommand : ICommand<TResult>
    {
        if (query is IValidationRequired)
        {
            var validationResult = await CallPerformValidatationAsync(query);

            if (!validationResult.IsValid)
                return Result.FailureResult(ValidationFailure.CreateForCommand<TCommand, TResult>(validationResult));
        }

        return await _commandExecutor.ExecuteAsync(query);
    }

    private async Task<Result> PerformExecutionAsync<TCommand>(TCommand query) where TCommand : ICommand
    {
        if (query is IValidationRequired)
        {
            var validationResult = await CallPerformValidatationAsync(query);

            if (!validationResult.IsValid)
                return Result.FailureResult(ValidationFailure.CreateForCommand<TCommand>(validationResult));
        }            

        return await _commandExecutor.ExecuteAsync(query);
    }

    /// <summary>
    /// Performs the validation
    /// </summary>
    /// <typeparam name="TCommand">The type of the query.</typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    private async Task<ValidationResult> PerformValidatationAsync<TCommand>(TCommand query) where TCommand : IValidationRequired
    {
        return await _validatorExecutor.ExecuteAsync(query);
    }
}
