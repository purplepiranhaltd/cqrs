using PurplePiranha.FluentResults.Results;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<Result> ExecuteAsync(TCommand command);
}

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> ExecuteAsync(TCommand command);
}