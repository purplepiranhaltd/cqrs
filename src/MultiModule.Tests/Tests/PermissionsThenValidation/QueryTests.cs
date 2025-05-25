using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Core.Extensions;
using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.PermissionsThenValidation.Query;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.MultiModule.Tests.Tests.PermissionsThenValidation;

public class QueryTests
{
    private IQueryExecutor _queryExecutor;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddCqrs()
            .WithCqrsValidationModule()
            .WithCqrsPermissionsModule()
            .AddLogging();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryExecutor = serviceProvider.GetRequiredService<IQueryExecutor>();
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