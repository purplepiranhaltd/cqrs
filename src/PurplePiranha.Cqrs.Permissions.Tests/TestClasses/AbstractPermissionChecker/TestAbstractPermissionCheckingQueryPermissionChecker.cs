using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.PermissionCheckers;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public class TestAbstractPermissionCheckingQueryPermissionChecker : 
    AbstractPermissionChecker<TestAbstractPermissionCheckingQuery>
{
    public override Task Permissions()
    {
        PermissionFor(x => x.IMustNotBe100).IsNotEqualTo(100).Grant();

        return Task.CompletedTask;
    }
}