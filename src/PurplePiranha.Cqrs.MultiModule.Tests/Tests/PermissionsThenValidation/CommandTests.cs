using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Extra.Tests.TestClasses.PermissionsThenValidation.Command;
using PurplePiranha.Cqrs.Permissions.Extensions;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Extra.Tests.Tests.PermissionsThenValidation
{
    public class CommandTests
    {
        private readonly ICommandExecutor _commandExecutor;

        public CommandTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddCqrs()
                .WithCqrsValidationModule()
                .WithCqrsPermissionsModule();

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
}