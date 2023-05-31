using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Queries;
using PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Tests;

public class QueryValidationExecutorUnitTests
{
    private readonly IQueryExecutor _queryExecutor;

    public QueryValidationExecutorUnitTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrsWithValidation();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryExecutor = serviceProvider.GetRequiredService<IQueryExecutor>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test_QueryExecutor_RunsQuery()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestValidatingQuery(1));
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task Test_QueryExecutor_QueryReturnsCorrectResult()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestValidatingQuery(2));
        Assert.That(result.Value, Is.EqualTo(4));
    }

    [Test]
    public void Test_QueryExecutor_QueryExecutorIsQueryExecutorWithValidation()
    {
        Assert.That(_queryExecutor, Is.TypeOf<QueryExecutorWithValidation>());
    }

    [Test]
    public async Task Test_QueryExecutor_QueryExecutorPerformsValidation()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestValidatingQuery(Int32.MaxValue));
        result.OnValidationFailure(v =>
        {
            Assert.Pass();
        });
        Assert.Fail();
    }
}