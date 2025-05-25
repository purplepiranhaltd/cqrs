using PurplePiranha.Cqrs.Permissions.Abstractions;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.PermissionsThenValidation.Command;

public class PThenVTestCommandPermissionChecker : IPermissionChecker<PThenVTestCommand>
{
    public Task<bool> HasPermissionAsync(PThenVTestCommand obj)
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
