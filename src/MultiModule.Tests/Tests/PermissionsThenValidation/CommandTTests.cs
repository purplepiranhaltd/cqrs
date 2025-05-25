using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.Cqrs.Core.Extensions;
using PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.PermissionsThenValidation.CommandT;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.MultiModule.Tests.Tests.PermissionsThenValidation;

public class CommandTTests
{
    private ICommandExecutor _commandExecutor;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddCqrs()
            .WithCqrsValidationModule()
            .WithCqrsPermissionsModule()
            .AddLogging();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();
    }

    [Test]
    public async Task Query_EnsurePermissionCheckingIsPerformed()
    {
        var command = new PThenVTestCommandT(100);
        var result = await _commandExecutor.ExecuteAsync(command);
        result.OnFailure(f =>
        {
            if (f is NotAuthorisedFailure nof)
                Assert.Pass();
        });

        Assert.Fail();
    }

    [Test]
    public async Task Query_EnsureValidationIsPerformed()
    {
        var command = new PThenVTestCommandT(200);
        var result = await _commandExecutor.ExecuteAsync(command);
        result.OnFailure(f =>
        {
            if (f is ValidationFailure vf)
                Assert.Pass();
        });

        Assert.Fail();
    }

    [Test]
    public async Task Query_EnsureHandlerIsExecuted()
    {
        var command = new PThenVTestCommandT(300);
        var result = await _commandExecutor.ExecuteAsync(command);
        result.OnSuccess(r =>
        {
            Assert.That(r, Is.EqualTo(300));
            Assert.Pass();
        });

        Assert.Fail();
    }
}