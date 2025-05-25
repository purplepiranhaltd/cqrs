using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.Cqrs.Core.Extensions;
using PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.PermissionsThenValidation.Command;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.MultiModule.Tests.Tests.PermissionsThenValidation;

public class CommandTests
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
        var command = new PThenVTestCommand(100);
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
        var command = new PThenVTestCommand(200);
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
        var command = new PThenVTestCommand(300);
        var result = await _commandExecutor.ExecuteAsync(command);
        result.OnSuccess(() =>
        {
            Assert.Pass();
        });

        Assert.Fail();
    }
}