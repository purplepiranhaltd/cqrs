using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Queries;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public record TestPermissionCheckingQuery(int IMustNotBe100) : IQuery<int>, IPermissionRequired
{
}