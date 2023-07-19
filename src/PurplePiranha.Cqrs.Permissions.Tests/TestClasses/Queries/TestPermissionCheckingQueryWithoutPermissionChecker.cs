using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Queries;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public record TestPermissionCheckingQueryWithoutPermissionChecker(int A) : IQuery<int>, IPermissionRequired
{
}