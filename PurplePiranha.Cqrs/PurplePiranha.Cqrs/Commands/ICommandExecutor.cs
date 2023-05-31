using PurplePiranha.FluentResults.Results;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands;

/// <summary>
/// Performs the function of executing a command via its correct handler.
/// </summary>
public interface ICommandExecutor
{
    Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;

    Task<Result<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command);
}