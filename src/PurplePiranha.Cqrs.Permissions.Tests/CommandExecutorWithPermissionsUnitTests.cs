using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Permissions.Exceptions;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Commands;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests;

public class CommandExecutorWithPermissionsUnitTests
{
    private ICommandExecutor _commandExecutor;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().WithCqrsPermissionsModule();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();
    }

    [Test]
    public async Task CommandExecutorWithPermissions_RunsCommand()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestPermissionCheckingCommand(4));
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task CommandExecutorWithPermissions_RunsCommandWithResult()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestPermissionCheckingCommandWithResult(4));
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task CommandExecutorWithPermissions_ReturnsCorrectResult()
    {
        var result = await _commandExecutor.ExecuteAsync<int>(new TestPermissionCheckingCommandWithResult(4));
        Assert.That(result.IsSuccess, Is.True);
        result.OnSuccess(r =>
        {
            Assert.That(r, Is.EqualTo(8));
            Assert.Pass();
        });

        Assert.Fail();
    }

    [Test]
    public async Task CommandExecutorWithPermissions_PerformsPermissionCheckingOnCommand()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestPermissionCheckingCommand(100));
        result.OnFailure(f =>
        {
            if (f is NotAuthorisedFailure nof)
            {
                Assert.Pass();
            }
        });
        Assert.Fail();
    }

    [Test]
    public async Task CommandExecutorWithPermissions_PerformsPermissionCheckingOnCommandT()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestPermissionCheckingCommandWithResult(100));
        result.OnFailure(f =>
        {
            if (f is NotAuthorisedFailure nof)
            {
                Assert.Pass();
            }
        });
        Assert.Fail();
    }

    [Test]
    public async Task CommandExecutorWithPermissions_ThrowsIfPermissionCheckerDoesNotExist()
    {
        Assert.ThrowsAsync<PermissionCheckerNotImplementedException>(async () =>
        {
            await _commandExecutor.ExecuteAsync(new TestPermissionCheckingCommandWithoutPermissionChecker(1));
        });
    }
}