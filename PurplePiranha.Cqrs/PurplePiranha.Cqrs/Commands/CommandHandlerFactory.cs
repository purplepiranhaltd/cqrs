using System;

namespace PurplePiranha.Cqrs.Commands;

public class CommandHandlerFactory : ICommandHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CommandHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandHandler<T> CreateHandler<T>() where T : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<T>));
        return (ICommandHandler<T>)handler;
    }
}
