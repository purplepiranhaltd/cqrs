using System;

namespace PurplePiranha.Cqrs.Core.Commands;

/// <summary>
/// Determines the correct command handler for the given command type
/// </summary>
/// <seealso cref="ICommandHandlerFactory" />
public class CommandHandlerFactory : ICommandHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CommandHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Creates the correct handler for the type of command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <returns></returns>
    public ICommandHandler<TCommand> CreateHandler<TCommand>() where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>));

        if (handler is null)
            throw CommandHandlerNotImplementedException.Create<TCommand>();

        return (ICommandHandler<TCommand>)handler;
    }

    /// <summary>
    /// Creates the correct handler for the type of command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <returns></returns>
    public ICommandHandler<TCommand, TResult> CreateHandler<TCommand, TResult>() where TCommand : ICommand<TResult>
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>));

        if (handler is null)
            throw CommandHandlerNotImplementedException.Create<TCommand, TResult>();

        return (ICommandHandler<TCommand, TResult>)handler;
    }
}