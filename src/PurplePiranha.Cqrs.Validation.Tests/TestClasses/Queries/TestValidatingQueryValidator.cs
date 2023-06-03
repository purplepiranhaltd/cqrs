using FluentValidation;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;
using PurplePiranha.FluentResults.Validation.Results;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;

public class TestValidatingQueryValidator : AbstractValidator<TestValidatingQuery>
{
    public TestValidatingQueryValidator()
    {
        RuleFor(q => q.A).LessThanOrEqualTo((int.MaxValue - 1) / 2);
    }
}