﻿using PurplePiranha.Cqrs.Permissions.Abstractions;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public class TestPermissionCheckingQueryPermissionChecker : IPermissionChecker<TestPermissionCheckingQuery>
{
    public Task<bool> HasPermissionAsync(TestPermissionCheckingQuery obj)
    {
        if (obj.IMustNotBe100 == 100)
            return Task.FromResult(false);

        else return Task.FromResult(true);
    }

    public Task InitialiseAsync()
    {
        return Task.CompletedTask;
    }
}