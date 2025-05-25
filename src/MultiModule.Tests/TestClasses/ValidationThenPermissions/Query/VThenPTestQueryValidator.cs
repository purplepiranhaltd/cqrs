using FluentValidation;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.ValidationThenPermissions.Query;

public class VThenPTestQueryValidator : AbstractValidator<VThenPTestQuery>
{
    public VThenPTestQueryValidator()
    {
        RuleFor(c => c.SpecialNumber).NotEqual(200);
    }
}
