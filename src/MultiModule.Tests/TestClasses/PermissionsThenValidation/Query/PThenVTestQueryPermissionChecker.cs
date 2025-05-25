using PurplePiranha.Cqrs.Permissions.Abstractions;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.PermissionsThenValidation.Query;

public class PThenVTestQueryPermissionChecker : IPermissionChecker<PThenVTestQuery>
{
    public Task<bool> HasPermissionAsync(PThenVTestQuery obj)
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
