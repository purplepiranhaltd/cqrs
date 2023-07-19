using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Extra.Tests.TestClasses.PermissionsThenValidation.Command;
using PurplePiranha.Cqrs.Extra.Tests.TestClasses.ValidationThenPermissions.Command;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Extra.Tests.Tests.ValidationThenPermissions
{
    public class CommandValidationAndPermissionsTests
    {
        private readonly ICommandExecutor _commandExecutor;

        public CommandValidationAndPermissionsTests()
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
            var command = new VThenPTestCommand(100);
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
            var command = new VThenPTestCommand(200);
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
            var command = new VThenPTestCommand(300);
            var result = await _commandExecutor.ExecuteAsync(command);
            result.OnSuccess(() =>
            {
                Assert.Pass();
            });

            Assert.Fail();
        }
    }
}