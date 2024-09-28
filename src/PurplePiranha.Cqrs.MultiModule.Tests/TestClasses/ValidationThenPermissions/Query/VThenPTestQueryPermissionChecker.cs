using PurplePiranha.Cqrs.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.ValidationThenPermissions.Query;

public class VThenPTestQueryPermissionChecker : IPermissionChecker<VThenPTestQuery>
{
    public Task<bool> HasPermissionAsync(VThenPTestQuery obj)
    {
        // Ensure that permission checker is not called before validator
        Assert.That(obj.SpecialNumber, Is.Not.EqualTo(200));

        if (obj.SpecialNumber == 100)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public Task InitialiseAsync()
    {
        return Task.CompletedTask;
    }
}
