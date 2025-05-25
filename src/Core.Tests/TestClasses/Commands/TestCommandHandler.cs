using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Core.Tests.TestClasses.Commands;

public class TestCommandHandler : ICommandHandler<TestCommand>
{
    public async Task<Result> ExecuteAsync(TestCommand command, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.SuccessResult());
    }
}