using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Validation.Queries;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;
using PurplePiranha.FluentResults.Validation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Validation.Commands
{
    public class ValidatingCommandExecutor : CommandExecutor, IValidatingCommandExecutor
    {
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

        public ValidatingCommandExecutor(IValidatorExecutor validatorExecutor, ICommandHandlerFactory commandHandlerFactory) : base(commandHandlerFactory)
        {
            _validatorExecutor = validatorExecutor;
        }

        async Task<ResultWithValidation> IValidatingCommandExecutor.ExecuteAsync<TCommand>(TCommand command)
        {
            return await PerformExecutionAsync<TCommand>(command);
        }

        async Task<ResultWithValidation<TResult>> IValidatingCommandExecutor.ExecuteAsync<TResult>(ICommand<TResult> command)
        {
            return await CallPerformExecuteTAsync<TResult>(command);
        }

        private async Task<ResultWithValidation<TResult>> CallPerformExecuteTAsync<TResult>(ICommand<TResult> command)
        {
            var commandType = command.GetType();
            var resultType = typeof(TResult);

            try
            {
                var method = PerformExecutionTAsyncMethod.MakeGenericMethod(commandType, resultType);

#nullable disable
                return await (Task<ResultWithValidation<TResult>>)method.Invoke(this, new object[] { command });
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

        private async Task<ResultWithValidation> CallPerformValidatationAsync<TCommand>(TCommand command)
        {
            var queryType = command.GetType();

            try
            {
                var method = PerformValidatationAsyncMethod.MakeGenericMethod(queryType);

#nullable disable
                return await (Task<ResultWithValidation>)method.Invoke(this, new object[] { command });
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

        private async Task<ResultWithValidation<TResult>> PerformExecutionTAsync<TCommand, TResult>(TCommand query) where TCommand : ICommand<TResult>
        {
            var validationResult = (query is IValidationRequired) ? await CallPerformValidatationAsync(query) : ResultWithValidation.SuccessResult();

            if (!validationResult.IsSuccess)
                return validationResult;

            return await base.ExecuteAsync(query);
        }

        private async Task<ResultWithValidation> PerformExecutionAsync<TCommand>(TCommand query) where TCommand : ICommand
        {
            var validationResult = (query is IValidationRequired) ? await CallPerformValidatationAsync(query) : ResultWithValidation.SuccessResult();

            if (!validationResult.IsSuccess)
                return validationResult;

            return await base.ExecuteAsync(query);
        }

        /// <summary>
        /// Performs the validation
        /// </summary>
        /// <typeparam name="TCommand">The type of the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private async Task<ResultWithValidation> PerformValidatationAsync<TCommand>(TCommand query) where TCommand : IValidationRequired
        {
            return await _validatorExecutor.ExecuteAsync(query);
        }
    }
}
