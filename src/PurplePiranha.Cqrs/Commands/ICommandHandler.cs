using PurplePiranha.FluentResults.Results;
using System.Threading;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands;

/// <summary>
/// Handles the execution of an individual command
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<Result> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
}

/// <summary>
/// Handles the execution of an individual command
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
}