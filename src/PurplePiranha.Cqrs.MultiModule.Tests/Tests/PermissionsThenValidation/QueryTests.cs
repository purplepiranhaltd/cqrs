using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Extra.Tests.TestClasses.PermissionsThenValidation.Query;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Extra.Tests.Tests.PermissionsThenValidation
{
    public class QueryTests
    {
        private readonly IQueryExecutor _queryExecutor;

        public QueryTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddCqrs()
                .WithCqrsValidationModule()
                .WithCqrsPermissionsModule();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _queryExecutor = serviceProvider.GetRequiredService<IQueryExecutor>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Query_EnsurePermissionCheckingIsPerformed()
        {
            var query = new PThenVTestQuery(100);
            var result = await _queryExecutor.ExecuteAsync(query);
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
            var query = new PThenVTestQuery(200);
            var result = await _queryExecutor.ExecuteAsync(query);
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
            var query = new PThenVTestQuery(300);
            var result = await _queryExecutor.ExecuteAsync(query);
            result.OnSuccess(r =>
            {
                Assert.That(r, Is.EqualTo(300));
                Assert.Pass();
            });

            Assert.Fail();
        }
    }
}