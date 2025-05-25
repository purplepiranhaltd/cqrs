using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.Cqrs.Permissions.Decorators;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.AbstractPermissionCheckerMultipleRules;

public record TestAbstractMPermissionCheckingQuery(bool userIsGuest, int userId) : IQuery<int>, IPermissionRequired
{
}