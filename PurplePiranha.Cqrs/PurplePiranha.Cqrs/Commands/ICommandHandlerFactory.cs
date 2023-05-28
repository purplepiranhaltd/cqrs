namespace PurplePiranha.Cqrs.Commands;

public interface ICommandHandlerFactory
{
    ICommandHandler<T> CreateHandler<T>() where T : ICommand;
}
