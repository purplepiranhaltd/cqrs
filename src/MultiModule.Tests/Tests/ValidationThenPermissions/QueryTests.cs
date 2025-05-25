using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Core.Extensions;
using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.ValidationThenPermissions.Query;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.MultiModule.Tests.Tests.ValidationThenPermissions;

public class QueryValidationAndPermissionsTests
{
    private readonly IQueryExecutor _queryExecutor;

    public QueryValidationAndPermissionsTests()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddCqrs()
            .WithCqrsPermissionsModule()
            .WithCqrsValidationModule()
            .AddLogging();

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
        var query = new VThenPTestQuery(100);
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
        var query = new VThenPTestQuery(200);
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
        var query = new VThenPTestQuery(300);
        var result = await _queryExecutor.ExecuteAsync(query);
        result.OnSuccess(r =>
        {
            Assert.That(r, Is.EqualTo(300));
            Assert.Pass();
        });

        Assert.Fail();
    }
}