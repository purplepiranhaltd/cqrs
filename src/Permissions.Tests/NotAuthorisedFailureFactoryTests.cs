using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Core.Extensions;
using PurplePiranha.Cqrs.Permissions.Factories;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses;


namespace PurplePiranha.Cqrs.Permissions.Tests;

public class NotAuthorisedFailureFactoryTests
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FailureFactory_ReturnsDefaultNotAuthorisedFailure()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().WithCqrsPermissionsModule().AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var failureFactory = serviceProvider.GetRequiredService<NotAuthorisedFailureFactory>();
        var failure = failureFactory.GetNotAuthorisedFailure();
        Assert.That(failure, Is.Not.Null);
        Assert.That(failure, Is.InstanceOf<NotAuthorisedFailure>());
        Assert.That(failure, Is.Not.InstanceOf<CustomNotAuthorisedFailure>());
    }

    [Test]
    public void FailureFactory_ReturnsCustomNotAuthorisedFailure()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().WithCqrsPermissionsModule(new CqrsPermissionsOptions() { NotAuthorisedFailureType = typeof(CustomNotAuthorisedFailure) });
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var failureFactory = serviceProvider.GetRequiredService<NotAuthorisedFailureFactory>();
        var failure = failureFactory.GetNotAuthorisedFailure();
        Assert.That(failure, Is.Not.Null);
        Assert.That(failure, Is.InstanceOf<CustomNotAuthorisedFailure>());
        Assert.That(failure, Is.Not.InstanceOf<NotAuthorisedFailure>());
    }
}
