using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;
using PurplePiranha.FluentResults.Validation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Validation.Commands
{
    /// <summary>
    /// Executes the command via the correct handler and performs validation beforehand if required.
    /// </summary>
    public interface IValidatingCommandExecutor : ICommandExecutor
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        new Task<ResultWithValidation> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        new Task<ResultWithValidation<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command);
    }
}
