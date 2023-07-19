using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Permissions.Exceptions;
using PurplePiranha.Cqrs.Permissions.Extensions;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests;

public class QueryExecutorWithPermissionsUnitTests
{
    private readonly IQueryExecutor _queryExecutor;

    public QueryExecutorWithPermissionsUnitTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().WithCqrsPermissionsModule();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryExecutor = serviceProvider.GetRequiredService<IQueryExecutor>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task QueryExecutorWithPermissions_RunsQuery()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestPermissionCheckingQuery(1));
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task QueryExecutorWithPermissions_ReturnsCorrectResult()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestPermissionCheckingQuery(2));
        Assert.That(result.Value, Is.EqualTo(4));
    }


    [Test]
    public async Task QueryExecutorWithPermissions_PerformsPermissionChecking()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestPermissionCheckingQuery(100));
        result.OnFailure(f =>
        {
            if (f is NotAuthorisedFailure naf)
            {
                Assert.Pass();
            }
        });
        Assert.Fail();
    }

    [Test]
    public async Task QueryExecutorWithPermissions_ThrowsIfValidatorDoesNotExist()
    {
        Assert.ThrowsAsync<PermissionCheckerNotImplementedException>(async () =>
        {
            await _queryExecutor.ExecuteAsync(new TestPermissionCheckingQueryWithoutPermissionChecker(1));
        });
    }
}