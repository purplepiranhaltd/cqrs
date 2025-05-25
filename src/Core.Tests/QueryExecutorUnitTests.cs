using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Core.Extensions;
using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.Cqrs.Core.Tests.TestClasses.Queries;

namespace PurplePiranha.Cqrs.Core.Tests;

public class QueryExecutorUnitTests
{
    private IQueryExecutor _queryExecutor;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryExecutor = serviceProvider.GetRequiredService<IQueryExecutor>();
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