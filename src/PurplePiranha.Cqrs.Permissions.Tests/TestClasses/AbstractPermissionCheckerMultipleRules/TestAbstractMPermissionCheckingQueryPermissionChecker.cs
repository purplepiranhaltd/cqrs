using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.PermissionCheckers;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public class TestAbstractMPermissionCheckingQueryPermissionChecker : 
    AbstractPermissionChecker<TestAbstractMPermissionCheckingQuery>
{
    public override Task Permissions()
    {
        PermissionFor(x => x.userIsGuest).IsEqualTo(true).Deny();
        PermissionFor(x => x.userId).IsEqualTo(1).Deny();
        PermissionFor(x => x.userId).IsEqualTo(2).Grant();

        return Task.CompletedTask;
    }
}