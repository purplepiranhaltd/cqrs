using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Results;

namespace PurplePiranha.Cqrs.Tests.Commands
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public async Task<Result> ExecuteAsync(TestCommand command)
        {
            return await Task.FromResult(Result.SuccessResult());
        }
    }
}
