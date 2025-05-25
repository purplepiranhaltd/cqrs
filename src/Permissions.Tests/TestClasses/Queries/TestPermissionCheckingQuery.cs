using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.Cqrs.Permissions.Decorators;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public record TestPermissionCheckingQuery(int IMustNotBe100) : IQuery<int>, IPermissionRequired
{
}