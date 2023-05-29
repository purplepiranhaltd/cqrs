using PurplePiranha.Cqrs.Exceptions;
using System;

namespace PurplePiranha.Cqrs.Commands;

public class CommandHandlerFactory : ICommandHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CommandHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandHandler<TCommand> CreateHandler<TCommand>() where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>));

        if (handler is null)
            throw new HandlerNotImplementedException($"Command handler '{nameof(TCommand)}' has not been implemented.");

        return (ICommandHandler<TCommand>)handler;
    }

    public ICommandHandler<TCommand, TResult> CreateHandler<TCommand, TResult>() where TCommand : ICommand<TResult>
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>));

        if (handler is null)
            throw new HandlerNotImplementedException($"Command handler '{nameof(TCommand)}' with result '{nameof(TResult)}' has not been implemented.");

        return (ICommandHandler<TCommand, TResult>)handler;
    }
}
