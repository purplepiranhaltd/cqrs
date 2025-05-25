using PurplePiranha.Cqrs.Permissions.Abstractions;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.PermissionsThenValidation.CommandT;

public class PThenVTestCommandTPermissionChecker : IPermissionChecker<PThenVTestCommandT>
{
    public Task<bool> HasPermissionAsync(PThenVTestCommandT obj)
    {
        if (obj.SpecialNumber == 100)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public Task InitialiseAsync()
    {
        return Task.CompletedTask;
    }
}
