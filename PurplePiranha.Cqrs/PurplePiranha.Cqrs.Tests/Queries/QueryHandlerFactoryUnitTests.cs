using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Queries;

namespace PurplePiranha.Cqrs.Tests.Queries
{
    public class QueryHandlerFactoryUnitTests
    {
        private readonly IQueryHandlerFactory _queryHandlerFactory;

        public QueryHandlerFactoryUnitTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCqrsServices();
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
    }
}