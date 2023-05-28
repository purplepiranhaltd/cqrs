using PurplePiranha.Cqrs.Results;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands;

public class CommandExecutor : ICommandExecutor
{
    private readonly ICommandHandlerFactory _commandHandlerFactory;

    public CommandExecutor(ICommandHandlerFactory commandHandlerFactory)
    {
        _commandHandlerFactory = commandHandlerFactory;
    }
    public async Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _commandHandlerFactory.CreateHandler<TCommand>();
        return await handler.ExecuteAsync(command);
    }

    public async Task<Result<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        var handler = _commandHandlerFactory.CreateHandler<TCommand, TResult>();
        return await handler.ExecuteAsync(command);
    }
}
