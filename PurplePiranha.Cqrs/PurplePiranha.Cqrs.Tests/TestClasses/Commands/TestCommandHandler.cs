using PurplePiranha.Cqrs.Commands;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Tests.TestClasses.Commands;

public class TestCommandHandler : ICommandHandler<TestCommand>
{
    public async Task<Result> ExecuteAsync(TestCommand command)
    {
        return await Task.FromResult(Result.SuccessResult());
    }
}
