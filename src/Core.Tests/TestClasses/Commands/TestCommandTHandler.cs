using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Core.Tests.TestClasses.Commands;

public class TestCommandTHandler : ICommandHandler<TestCommandT, int>
{
    public async Task<Result<int>> ExecuteAsync(TestCommandT command, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.SuccessResult(15));
    }
}