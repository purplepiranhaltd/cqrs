using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;
using PurplePiranha.FluentResults.Validation.Results;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;

public class TestValidatingQueryValidator : IValidator<TestValidatingQuery>
{
    public async Task<ResultWithValidation> ValidateAsync(TestValidatingQuery query)
    {
        var failures = new List<string>();

        if (query.A > (int.MaxValue - 1) / 2)
        {
            failures.Add("Input would cause overflow.");
            return await Task.FromResult(ResultWithValidation.ValidationFailureResult(failures));
        }

        return await Task.FromResult(ResultWithValidation.SuccessResult());
    }
}