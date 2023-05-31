using PurplePiranha.FluentResults.Results;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands;

/// <summary>
/// Executes a command via the correct handler.
/// </summary>
/// <seealso cref="PurplePiranha.Cqrs.Commands.ICommandExecutor" />
public class CommandExecutor : ICommandExecutor
{
    private readonly ICommandHandlerFactory _commandHandlerFactory;

#nullable disable
    private static MethodInfo ExecuteCommandAsyncMethod =>
        typeof(CommandExecutor)
            .GetMethod(
                nameof(ExecuteCommandAsync),
                BindingFlags.NonPublic | BindingFlags.Instance
            );
#nullable enable

    public CommandExecutor(ICommandHandlerFactory commandHandlerFactory)
    {
        _commandHandlerFactory = commandHandlerFactory;
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="command">The command.</param>
    /// <returns></returns>
    public virtual async Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        try
        {
            var handler = _commandHandlerFactory.CreateHandler<TCommand>();
            return await handler.ExecuteAsync(command);
        }
        catch (CommandHandlerNotImplementedException e)
        {
            return await Task.FromResult(Result.ErrorResult(CommandErrors.CommandHandlerNotImplemented));
        }
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="command">The command.</param>
    /// <returns></returns>
    public async Task<Result<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command)
    {
        // We make a dynamic call to the generic method via reflection,
        // otherwise the return type would have to be specified on every call

        var commandType = command.GetType();
        var resultType = typeof(TResult);

        try
        {
            var method = ExecuteCommandAsyncMethod.MakeGenericMethod(commandType, resultType);

#nullable disable
            return await (Task<Result<TResult>>)method.Invoke(this, new object[] { command });
#nullable enable

        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException is null)
                throw;

            var info = ExceptionDispatchInfo.Capture(ex.InnerException);
            info.Throw();

            // compiler requires assignment - an exception is always thrown so we can never get here
            return default;
        }
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="command">The command.</param>
    /// <returns></returns>
    protected virtual async Task<Result<TResult>> ExecuteCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        try
        {
            var handler = _commandHandlerFactory.CreateHandler<TCommand, TResult>();
            return await handler.ExecuteAsync(command);
        }
        catch (CommandHandlerNotImplementedException e)
        {
            return await Task.FromResult(Result.ErrorResult<TResult>(CommandErrors.CommandHandlerNotImplemented));
        }
    }
}