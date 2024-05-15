using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Tests.TestClasses.Commands;

namespace PurplePiranha.Cqrs.Tests;

public class CommandHandlerExecutorUnitTests
{
    private ICommandExecutor _commandExecutor;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();
    }

    [Test]
    public async Task Test_CommandExecutor_RunsCommand()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestCommand());
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task Test_CommandExecutor_RunsCommandWithResult()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestCommandT());
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task Test_CommandExecutor_CommandWithResultReturnsCorrectResult()
    {
        var result = await _commandExecutor.ExecuteAsync(new TestCommandT());
        Assert.That(result.Value, Is.EqualTo(15));
    }
}