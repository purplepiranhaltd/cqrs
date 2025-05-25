using PurplePiranha.FluentResults.Results;
using System.Threading;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Core.Commands;

/// <summary>
/// Performs the function of executing a command via its correct handler.
/// </summary>
public interface ICommandExecutor
{
    Task<Result> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;

    Task<Result<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
}