using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.Cqrs.Core.Extensions;
using PurplePiranha.Cqrs.Core.Tests.TestClasses.Commands;

namespace PurplePiranha.Cqrs.Core.Tests;

public class CommandHandlerFactoryUnitTests
{
    private ICommandHandlerFactory _commandHandlerFactory;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _commandHandlerFactory = serviceProvider.GetRequiredService<ICommandHandlerFactory>();
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