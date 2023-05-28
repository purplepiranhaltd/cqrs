using PurplePiranha.Cqrs.Results;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands;

public interface ICommandExecutor
{
    Task<Result> ExecuteAsync<T>(T command) where T : ICommand;
}
