﻿using PurplePiranha.Cqrs.Permissions.Abstractions;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.ValidationThenPermissions.CommandT;

public class VThenPTestCommandTPermissionChecker : IPermissionChecker<VThenPTestCommandT>
{
    public Task<bool> HasPermissionAsync(VThenPTestCommandT obj)
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
