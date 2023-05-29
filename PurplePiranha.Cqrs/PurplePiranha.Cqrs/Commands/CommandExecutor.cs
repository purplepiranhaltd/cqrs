using PurplePiranha.Cqrs.Errors;
using PurplePiranha.Cqrs.Exceptions;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Results;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands;

public class CommandExecutor : ICommandExecutor
{
    private readonly ICommandHandlerFactory _commandHandlerFactory;
    private static readonly MethodInfo _executeAsyncMethod = typeof(CommandExecutor).GetMethod(nameof(ExecuteCommandAsync), BindingFlags.NonPublic | BindingFlags.Instance);


    public CommandExecutor(ICommandHandlerFactory commandHandlerFactory)
    {
        _commandHandlerFactory = commandHandlerFactory;
    }
    public async Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        try
        {
            var handler = _commandHandlerFactory.CreateHandler<TCommand>();
            return await handler.ExecuteAsync(command);
        }
        catch(HandlerNotImplementedException e)
        {
            return await Task.FromResult(Result.ErrorResult(CommandErrors.CommandHandlerNotImplemented));
        }
    }

    public async Task<Result<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command)
    {
        Result<TResult> result;

        if (command == null) return default;
        try
        {
            var commandType = command.GetType();
            var resultType = typeof(TResult);
            result = await (Task<Result<TResult>>)_executeAsyncMethod
                .MakeGenericMethod(commandType, resultType)
                .Invoke(this, new object[] { command });
        }
        catch (TargetInvocationException ex)
        {
            result = HandleException<TResult>(ex);
        }

        return result;
    }

    private async Task<Result<TResult>> ExecuteCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        try
        {
            var handler = _commandHandlerFactory.CreateHandler<TCommand, TResult>();
            return await handler.ExecuteAsync(command);
        }
        catch (HandlerNotImplementedException e)
        {
            return await Task.FromResult(Result.ErrorResult<TResult>(CommandErrors.CommandHandlerNotImplemented));
        }
    }

    private TResult HandleException<TResult>(TargetInvocationException ex)
    {
        var info = ExceptionDispatchInfo.Capture(ex.InnerException);
        info.Throw();

        // compiler requires assignment
        return default;
    }
}
