using PurplePiranha.Cqrs.Permissions.Abstractions;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Commands;

public class TestPermissionCheckingCommandPermissionChecker : 
    IPermissionChecker<TestPermissionCheckingCommandWithResult>,
    IPermissionChecker<TestPermissionCheckingCommand>
{
    public Task<bool> HasPermission(TestPermissionCheckingCommandWithResult obj)
    {
        return HasPermission(obj.IMustNotBe100);
    }

    public Task<bool> HasPermission(TestPermissionCheckingCommand obj)
    {
        return HasPermission(obj.IMustNotBe100);
    }

    private Task<bool> HasPermission(int mustNotBe100)
    {
        if (mustNotBe100 == 100)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }
}
