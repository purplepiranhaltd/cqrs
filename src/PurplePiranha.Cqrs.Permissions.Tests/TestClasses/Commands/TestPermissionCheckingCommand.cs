using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Permissions.Decorators;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Commands
{
    public record TestPermissionCheckingCommand(int IMustNotBe100) : ICommand, IPermissionRequired
    {
    }

    public record TestPermissionCheckingCommandWithResult(int IMustNotBe100) : ICommand<int>, IPermissionRequired
    {
    }
}
