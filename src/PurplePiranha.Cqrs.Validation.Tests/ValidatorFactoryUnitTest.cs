using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;
using PurplePiranha.Cqrs.Validation.Validators;

namespace PurplePiranha.Cqrs.Validation.Tests;

public class ValidatorFactoryUnitTest
{
    private readonly IValidatorFactory _queryValidationHandlerFactory;

    public ValidatorFactoryUnitTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().WithCqrsValidation();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryValidationHandlerFactory = serviceProvider.GetRequiredService<IValidatorFactory>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ValidatorFactory_ResolvesValidator()
    {
        var handler = _queryValidationHandlerFactory.CreateValidator<TestValidatingQuery>();
        Assert.That(handler, Is.Not.Null);
        Assert.That(handler.GetType(), Is.EqualTo(typeof(TestValidatingQueryValidator)));
    }

    [Test]
    public void ValidatorFactory_ThrowsExceptionWhenValidatorDoesNotExist()
    {
        Assert.Throws<ValidatorNotImplementedException>(() =>
        {
            var handler = _queryValidationHandlerFactory.CreateValidator<TestValidatingQueryWithoutValidationHandler>();
        });
    }
}