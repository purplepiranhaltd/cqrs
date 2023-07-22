using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Builders;
using PurplePiranha.Cqrs.Permissions.Exceptions;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests;

public class PermissionBuilderUnitTests
{
    private PermissionBuilder _permissionBuilder;

    [SetUp]
    public void Setup()
    {
        _permissionBuilder = new PermissionBuilder();
    }

    [Test]
    public async Task IsTrue_EqualsTrue_Grant_OutcomeEqualsGrant()
    {
        _permissionBuilder.IsTrue(() => true).Grant();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task IsTrue_EqualsTrue_Deny_OutcomeEqualsDeny()
    {
        _permissionBuilder.IsTrue(() => true).Deny();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Deny));
    }

    [Test]
    public async Task IsTrue_EqualsFalse_Grant_OutcomeEqualsNone()
    {
        _permissionBuilder.IsTrue(() => false).Grant();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsTrue_EqualsFalse_Deny_OutcomeEqualsNone()
    {
        _permissionBuilder.IsTrue(() => false).Deny();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsTrue_EqualsTrueAsync_Grant_OutcomeEqualsGrant()
    {
        _permissionBuilder.IsTrue(async () => await Task.FromResult(true)).Grant();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task IsTrue_EqualsTrueAsync_Deny_OutcomeEqualsDeny()
    {
        _permissionBuilder.IsTrue(async () => await Task.FromResult(true)).Deny();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Deny));
    }

    [Test]
    public async Task IsTrue_EqualsFalseAsync_Grant_OutcomeEqualsNone()
    {
        _permissionBuilder.IsTrue(async () => await Task.FromResult(false)).Grant();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsTrue_EqualsFalseAsync_Deny_OutcomeEqualsNone()
    {
        _permissionBuilder.IsTrue(async () => await Task.FromResult(false)).Deny();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsFalse_EqualsFalse_Grant_OutcomeEqualsGrant()
    {
        _permissionBuilder.IsFalse(() => false).Grant();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task IsFalse_EqualsFalse_Deny_OutcomeEqualsDeny()
    {
        _permissionBuilder.IsFalse(() => false).Deny();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Deny));
    }

    [Test]
    public async Task IsFalse_EqualsTrue_Grant_OutcomeEqualsNone()
    {
        _permissionBuilder.IsFalse(() => true).Grant();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsFalse_EqualsTrue_Deny_OutcomeEqualsNone()
    {
        _permissionBuilder.IsFalse(() => true).Deny();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsFalse_EqualsFalseAsync_Grant_OutcomeEqualsGrant()
    {
        _permissionBuilder.IsFalse(async () => await Task.FromResult(false)).Grant();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task IsFalse_EqualsFalseAsync_Deny_OutcomeEqualsDeny()
    {
        _permissionBuilder.IsFalse(async () => await Task.FromResult(false)).Deny();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Deny));
    }

    [Test]
    public async Task IsFalse_EqualsTrueAsync_Grant_OutcomeEqualsNone()
    {
        _permissionBuilder.IsFalse(async () => await Task.FromResult(true)).Grant();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsFalse_EqualsTrueAsync_Deny_OutcomeEqualsNone()
    {
        _permissionBuilder.IsFalse(async () => await Task.FromResult(true)).Deny();
        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsTrue_EqualsTrue_Or_IsTrue_EqualsTrue_Grant_OutcomeEqualsGrant()
    {
        _permissionBuilder
            .IsTrue(() => true)
            .Or()
            .IsTrue(() => true)
            .Grant();

        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task IsTrue_EqualsTrue_Or_IsTrue_EqualsFalse_Grant_OutcomeEqualsGrant()
    {
        _permissionBuilder
            .IsTrue(() => true)
            .Or()
            .IsTrue(() => false)
            .Grant();

        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task IsTrue_EqualsFalse_Or_IsTrue_EqualsTrue_Grant_OutcomeEqualsGrant()
    {
        _permissionBuilder
            .IsTrue(() => false)
            .Or()
            .IsTrue(() => true)
            .Grant();

        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }


    [Test]
    public async Task IsTrue_EqualsFalse_Or_IsTrue_EqualsFalse_Grant_OutcomeEqualsNone()
    {
        _permissionBuilder
            .IsTrue(() => false)
            .Or()
            .IsTrue(() => false)
            .Grant();

        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsTrue_EqualsTrue_And_IsTrue_EqualsTrue_Grant_OutcomeEqualsGrant()
    {
        _permissionBuilder
            .IsTrue(() => true)
            .And()
            .IsTrue(() => true)
            .Grant();

        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task IsTrue_EqualsTrue_And_IsTrue_EqualsFalse_Grant_OutcomeEqualsNone()
    {
        _permissionBuilder
            .IsTrue(() => true)
            .And()
            .IsTrue(() => false)
            .Grant();

        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task IsTrue_EqualsFalse_And_IsTrue_EqualsTrue_Grant_OutcomeEqualsNone()
    {
        _permissionBuilder
            .IsTrue(() => false)
            .And()
            .IsTrue(() => true)
            .Grant();

        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }


    [Test]
    public async Task IsTrue_EqualsFalse_And_IsTrue_EqualsFalse_Grant_OutcomeEqualsNone()
    {
        _permissionBuilder
            .IsTrue(() => false)
            .And()
            .IsTrue(() => false)
            .Grant();

        await _permissionBuilder.Build();
        Assert.That(_permissionBuilder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }
}