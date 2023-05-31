using PurplePiranha.Cqrs.Validation.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;

public class TestValidatingQueryValidator : IQueryValidationHandler<TestValidatingQuery>
{
    public async Task<Result> ValidateAsync(TestValidatingQuery query)
    {
        var failures = new List<string>();

        if (query.A > (int.MaxValue - 1) / 2)
        {
            failures.Add("Input would cause overflow.");
            return await Task.FromResult(Result.ValidationFailureResult(failures));
        }

        return await Task.FromResult(Result.SuccessResult());
    }
}