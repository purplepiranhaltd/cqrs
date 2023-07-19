using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Extra.Tests.TestClasses.ValidationThenPermissions.CommandT;
using PurplePiranha.Cqrs.Permissions.Extensions;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Extra.Tests.Tests.ValidationThenPermissions
{
    public class CommandTValidationAndPermissionsTests
    {
        private readonly ICommandExecutor _commandExecutor;

        public CommandTValidationAndPermissionsTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddCqrs()
                .WithCqrsPermissionsModule()
                .WithCqrsValidationModule();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Query_EnsurePermissionCheckingIsPerformed()
        {
            var command = new VThenPTestCommandT(100);
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
            var command = new VThenPTestCommandT(200);
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
            var command = new VThenPTestCommandT(300);
            var result = await _commandExecutor.ExecuteAsync(command);
            result.OnSuccess(r =>
            {
                Assert.That(r, Is.EqualTo(300));
                Assert.Pass();
            });

            Assert.Fail();
        }
    }
}