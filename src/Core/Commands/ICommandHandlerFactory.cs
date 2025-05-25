namespace PurplePiranha.Cqrs.Core.Commands;

/// <summary>
/// Determines the correct command handler for the given command type
/// </summary>
public interface ICommandHandlerFactory
{
    /// <summary>
    /// Creates the correct handler for the type of command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <returns></returns>
    ICommandHandler<TCommand> CreateHandler<TCommand>() where TCommand : ICommand;

    /// <summary>
    /// Creates the correct handler for the type of command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <returns></returns>
    ICommandHandler<TCommand, TResult> CreateHandler<TCommand, TResult>() where TCommand : ICommand<TResult>;
}