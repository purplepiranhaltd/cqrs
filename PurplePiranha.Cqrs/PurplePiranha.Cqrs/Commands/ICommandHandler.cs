using PurplePiranha.Cqrs.Results;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<Result> ExecuteAsync(TCommand command);
}
