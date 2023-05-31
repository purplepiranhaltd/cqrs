using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Exceptions;
using PurplePiranha.Cqrs.Queries.Validation;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;

namespace PurplePiranha.Cqrs.Validation.Tests;

public class QueryValidationHandlerFactoryUnitTests
{
    private readonly IQueryValidatorFactory _queryValidationHandlerFactory;

    public QueryValidationHandlerFactoryUnitTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrsWithValidation();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryValidationHandlerFactory = serviceProvider.GetRequiredService<IQueryValidatorFactory>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_QueryValidationHandlerFactory_ResolvesQueryValidationHandler()
    {
        var handler = _queryValidationHandlerFactory.CreateValidator<TestValidatingQuery>();
        Assert.That(handler, Is.Not.Null);
        Assert.That(handler.GetType(), Is.EqualTo(typeof(TestValidatingQueryValidator)));
    }

    [Test]
    public void Test_QueryValidationHandlerFactory_ThrowsExceptionWhenHandlerDoesNotExist()
    {
        Assert.Throws<QueryValidationHandlerNotImplementedException>(() =>
        {
            var handler = _queryValidationHandlerFactory.CreateValidator<TestValidatingQueryWithoutValidationHandler>();
        });
    }
}