using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.Cqrs.Permissions.Decorators;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public record TestPermissionCheckingQueryWithoutPermissionChecker(int A) : IQuery<int>, IPermissionRequired
{
}