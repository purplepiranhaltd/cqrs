using FluentValidation;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands;

public class TestValidatingCommandValidator : AbstractValidator<TestValidatingCommand>
{
    public TestValidatingCommandValidator()
    {
        RuleFor(c => c.IMustNotBe100).NotEqual(100);
    }
}
