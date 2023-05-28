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
    public async Task<Result> ExecuteAsync<T>(T command) where T : ICommand
    {
        var handler = _commandHandlerFactory.CreateHandler<T>();
        return await handler.ExecuteAsync(command);
    }
}
