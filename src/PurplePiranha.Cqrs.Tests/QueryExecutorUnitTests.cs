using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Tests.TestClasses.Queries;

namespace PurplePiranha.Cqrs.Tests;

public class QueryExecutorUnitTests
{
    private readonly IQueryExecutor _queryExecutor;

    public QueryExecutorUnitTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs();
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
        var result = await _queryExecutor.ExecuteAsync(new TestQuery(1));
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task Test_QueryExecutor_QueryReturnsCorrectResult()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestQuery(1));
        Assert.That(result.Value, Is.EqualTo("One"));
    }
}