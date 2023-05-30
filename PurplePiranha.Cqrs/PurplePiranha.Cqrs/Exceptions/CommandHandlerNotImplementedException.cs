using System;

namespace PurplePiranha.Cqrs.Exceptions;

public class CommandHandlerNotImplementedException : Exception
{
    public CommandHandlerNotImplementedException()
    {
    }

    public CommandHandlerNotImplementedException(string message)
        : base(message)
    {
    }

    public CommandHandlerNotImplementedException(string message, Exception inner)
    : base(message, inner)
    {
    }

    public CommandHandlerNotImplementedException(Type commandType) :
        base($"Command handler for command '{commandType.Name}' has not been implemented.")
    {
    }

    public CommandHandlerNotImplementedException(Type commandType, Type resultType) : 
        base($"Command handler for command '{commandType.Name}' with result type '{resultType.Name}' has not been implemented.")
    {
    }

    public static CommandHandlerNotImplementedException Create<TCommand>()
    {
        return new CommandHandlerNotImplementedException(typeof(TCommand));
    }
    public static CommandHandlerNotImplementedException Create<TCommand, TResult>()
    {
        return new CommandHandlerNotImplementedException(typeof(TCommand), typeof(TResult));
    }
}
