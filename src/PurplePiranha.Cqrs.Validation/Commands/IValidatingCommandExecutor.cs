using PurplePiranha.Cqrs.Commands;
using PurplePiranha.FluentResults.Results;

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
        new Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        new Task<Result<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command);
    }
}
