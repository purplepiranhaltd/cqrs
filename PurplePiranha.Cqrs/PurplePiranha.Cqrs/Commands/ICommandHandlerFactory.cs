using PurplePiranha.Cqrs.Results;

namespace PurplePiranha.Cqrs.Commands;

public interface ICommandHandlerFactory
{
    ICommandHandler<TCommand> CreateHandler<TCommand>() where TCommand : ICommand;
    ICommandHandler<TCommand, TResult> CreateHandler<TCommand, TResult>() where TCommand: ICommand<TResult>;
}
