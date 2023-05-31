using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Tests.TestClasses.Queries;

namespace PurplePiranha.Cqrs.Tests;

public class QueryHandlerFactoryUnitTests
{
    private readonly IQueryHandlerFactory _queryHandlerFactory;

    public QueryHandlerFactoryUnitTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryHandlerFactory = serviceProvider.GetRequiredService<IQueryHandlerFactory>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_QueryHandlerFactory_ResolvesQueryHandler()
    {
        var handler = _queryHandlerFactory.CreateHandler<TestQuery, string>();
        Assert.That(handler, Is.Not.Null);
        Assert.That(handler.GetType(), Is.EqualTo(typeof(TestQueryHandler)));
    }

    [Test]
    public void Test_QueryHandlerFactory_ThrowsExceptionWhenHandlerDoesNotExist()
    {
        Assert.Throws<QueryHandlerNotImplementedException>(() =>
        {
            var handler = _queryHandlerFactory.CreateHandler<TestQueryWithoutHandler, string>();
        });
    }
}