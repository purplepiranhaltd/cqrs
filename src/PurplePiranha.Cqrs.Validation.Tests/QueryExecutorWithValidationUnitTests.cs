using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Validation.Extensions;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.Cqrs.Validation.Queries;
using PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Tests;

public class QueryExecutorWithValidationUnitTests
{
    private readonly IValidatingQueryExecutor _queryExecutor;

    public QueryExecutorWithValidationUnitTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrsWithValidation();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryExecutor = serviceProvider.GetRequiredService<IValidatingQueryExecutor>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task QueryExecutor_RunsQuery()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestValidatingQuery(1));
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task QueryExecutor_ReturnsCorrectResult()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestValidatingQuery(2));
        Assert.That(result.Value, Is.EqualTo(4));
    }


    [Test]
    public async Task QueryExecutor_PerformsValidation()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestValidatingQuery(Int32.MaxValue));
        result.OnFailure(f =>
        {
            if (f is ValidationFailure vf)
            {
                Assert.Pass();
            }
        });
        Assert.Fail();
    }

    [Test]
    public async Task QueryExecutor_ThrowsIfValidatorDoesNotExist()
    {
        Assert.ThrowsAsync<ValidatorNotImplementedException>(async () =>
        {
            await _queryExecutor.ExecuteAsync(new TestValidatingQueryWithoutValidationHandler(1));
        });
    }
}