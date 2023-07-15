using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Validation.Commands;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Tests;

public class CommandExecutorWithValidationUnitTests
{
    private readonly IValidatingCommandExecutor _commandExecutor;

    public CommandExecutorWithValidationUnitTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrsWithValidation();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _commandExecutor = serviceProvider.GetRequiredService<IValidatingCommandExecutor>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CommandExecutorWithValidation_RunsCommand()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestValidatingCommand(4));
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task CommandExecutorWithValidation_ReturnsCorrectResult()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestValidatingCommand(4));
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task CommandExecutorWithValidation_PerformsValidation()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestValidatingCommand(100));
        result.OnFailure(f =>
        {
            if (f is ValidationFailure vf)
            {
                Assert.Pass();
            }
        });
        Assert.Fail();
    }

    [Test]
    public async Task CommandExecutor_ThrowsIfValidatorDoesNotExist()
    {
        Assert.ThrowsAsync<ValidatorNotImplementedException>(async () =>
        {
            await _commandExecutor.ExecuteAsync(new TestValidatingCommandWithoutValidationHandler(1));
        });
    }
}