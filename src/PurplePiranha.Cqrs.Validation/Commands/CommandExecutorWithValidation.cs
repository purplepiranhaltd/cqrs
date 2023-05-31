using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Validation.Queries;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Errors;
using PurplePiranha.FluentResults.Results;
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
    public class CommandExecutorWithValidation : CommandExecutor
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly IValidatorExecutor _validatorExecutor;

#nullable disable
        private static readonly MethodInfo ExecuteValidationAsyncMethod =
            typeof(CommandExecutorWithValidation)
                .GetMethod(
                    nameof(ExecuteValidationAsync),
                    BindingFlags.NonPublic | BindingFlags.Instance
                );
#nullable enable

        public CommandExecutorWithValidation(ICommandHandlerFactory commandHandlerFactory, IValidatorExecutor queryValidatorExecutor) : base(commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _validatorExecutor = queryValidatorExecutor;
        }

        public override async Task<Result> ExecuteAsync<TCommand>(TCommand command)
        {
            Result result = Result.ErrorResult(Error.None);

            var validationResult = (command is IValidationRequired) ? await CallExecuteValidatorAsync<TCommand>(command) : Result.SuccessResult();

            if (!validationResult.IsSuccess)
                return validationResult;

            return await base.ExecuteAsync(command);
        }

        protected override async Task<Result<TResult>> ExecuteCommandAsync<TCommand, TResult>(TCommand command)
        {
            Result result = Result.ErrorResult(Error.None);

            var validationResult = (command is IValidationRequired) ? await CallExecuteValidatorAsync<TCommand>(command) : Result.SuccessResult();

            if (!validationResult.IsSuccess)
                return validationResult.ToTypedResult<TResult>();

            return await base.ExecuteCommandAsync<TCommand, TResult>(command);
        }

        /// <summary>
        /// Performs the validation.
        /// </summary>
        /// <typeparam name="TCommand">The type of the query.</typeparam>
        /// <param name="command">The query.</param>
        /// <returns></returns>
        private async Task<Result> CallExecuteValidatorAsync<TCommand>(TCommand command)
        {
            // We make a dynamic call to the generic method via reflection,
            // otherwise the return type would have to be specified on every call

#nullable disable
            var commandType = command.GetType();
#nullable enable

            try
            {
                var method = ExecuteValidationAsyncMethod.MakeGenericMethod(commandType);

#nullable disable
                return await (Task<Result>)method.Invoke(this, new object[] { command });
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

        // <summary>
        /// Performs the validation
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <param name="query">The command.</param>
        /// <returns></returns>
        private async Task<Result> ExecuteValidationAsync<TCommand>(TCommand command) where TCommand : IValidationRequired
        {
            return await _validatorExecutor.ExecuteAsync(command);
        }
    }
}
