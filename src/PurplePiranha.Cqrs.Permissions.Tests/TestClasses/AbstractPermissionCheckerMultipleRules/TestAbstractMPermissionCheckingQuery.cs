using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Queries;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public record TestAbstractMPermissionCheckingQuery(bool userIsGuest, int userId) : IQuery<int>, IPermissionRequired
{
}