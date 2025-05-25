using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.Cqrs.Permissions.Decorators;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.AbstractPermissionChecker;

public record TestAbstractPermissionCheckingQuery(int IMustNotBe100) : IQuery<int>, IPermissionRequired
{
}