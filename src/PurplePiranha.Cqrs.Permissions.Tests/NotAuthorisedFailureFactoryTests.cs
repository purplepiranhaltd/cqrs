using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Permissions.Exceptions;
using PurplePiranha.Cqrs.Permissions.Factories;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Commands;
using PurplePiranha.FluentResults.Results;


namespace PurplePiranha.Cqrs.Permissions.Tests
{
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
            serviceCollection.AddCqrs().WithCqrsPermissionsModule();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var failureFactory = serviceProvider.GetRequiredService<NotAuthorisedFailureFactory>();
            var failure = failureFactory.GetNotAuthorisedFailure();
            Assert.IsNotNull(failure);
            Assert.IsInstanceOf<NotAuthorisedFailure>(failure);
            Assert.IsNotInstanceOf<CustomNotAuthorisedFailure>(failure);
        }

        [Test]
        public void FailureFactory_ReturnsCustomNotAuthorisedFailure()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCqrs().WithCqrsPermissionsModule(new Options.CqrsPermissionsOptions() { NotAuthorisedFailureType = typeof(CustomNotAuthorisedFailure) });
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var failureFactory = serviceProvider.GetRequiredService<NotAuthorisedFailureFactory>();
            var failure = failureFactory.GetNotAuthorisedFailure();
            Assert.IsNotNull(failure);
            Assert.IsInstanceOf<CustomNotAuthorisedFailure>(failure);
            Assert.IsNotInstanceOf<NotAuthorisedFailure>(failure);
        }
    }
}
