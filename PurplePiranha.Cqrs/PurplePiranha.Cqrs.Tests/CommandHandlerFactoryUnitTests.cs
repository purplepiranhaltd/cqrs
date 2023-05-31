using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Tests.TestClasses.Commands;

namespace PurplePiranha.Cqrs.Tests;

public class CommandHandlerFactoryUnitTests
{
    private readonly ICommandHandlerFactory _commandHandlerFactory;

    public CommandHandlerFactoryUnitTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _commandHandlerFactory = serviceProvider.GetRequiredService<ICommandHandlerFactory>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_CommandHandlerFactory_ResolvesCommandHandler()
    {
        var handler = _commandHandlerFactory.CreateHandler<TestCommand>();
        Assert.That(handler, Is.Not.Null);
        Assert.That(handler.GetType(), Is.EqualTo(typeof(TestCommandHandler)));
    }

    [Test]
    public void Test_CommandHandlerFactory_ThrowsExceptionWhenHandlerDoesNotExist()
    {
        Assert.Throws<CommandHandlerNotImplementedException>(() =>
        {
            var handler = _commandHandlerFactory.CreateHandler<TestCommandWithoutHandler>();
        });
    }

    [Test]
    public void Test_CommandHandlerFactory_ResolvesCommandHandlerWithResult()
    {
        var handler = _commandHandlerFactory.CreateHandler<TestCommandT, int>();
        Assert.That(handler, Is.Not.Null);
        Assert.That(handler.GetType(), Is.EqualTo(typeof(TestCommandTHandler)));
    }
}