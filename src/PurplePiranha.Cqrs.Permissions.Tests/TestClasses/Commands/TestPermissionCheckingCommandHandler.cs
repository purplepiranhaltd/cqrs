using PurplePiranha.Cqrs.Commands;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Commands;

public class TestPermissionCheckingCommandHandler : 
    ICommandHandler<TestPermissionCheckingCommandWithResult, int>,
    ICommandHandler<TestPermissionCheckingCommand>
{
    public Task<Result<int>> ExecuteAsync(TestPermissionCheckingCommandWithResult command, CancellationToken cancellationToken = default)
    {
        if (command.IMustNotBe100 == 100)
            Assert.Fail("Command handler called before permission checking.");

        return Task.FromResult(Result.SuccessResult(command.IMustNotBe100 * 2));
    }

    public Task<Result> ExecuteAsync(TestPermissionCheckingCommand command, CancellationToken cancellationToken = default)
    {
        if (command.IMustNotBe100 == 100)
            Assert.Fail("Command handler called before permission checking.");

        return Task.FromResult(Result.SuccessResult());
    }
}
