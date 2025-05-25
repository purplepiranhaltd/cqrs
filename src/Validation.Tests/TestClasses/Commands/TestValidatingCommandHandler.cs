using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands;

public class TestValidatingCommandHandler : ICommandHandler<TestValidatingCommand>
{
    public async Task<Result> ExecuteAsync(TestValidatingCommand command, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.SuccessResult());
    }
}
