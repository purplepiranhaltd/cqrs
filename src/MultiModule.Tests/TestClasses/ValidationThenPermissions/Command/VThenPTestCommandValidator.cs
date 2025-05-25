using FluentValidation;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.ValidationThenPermissions.Command;

public class VThenPTestCommandValidator : AbstractValidator<VThenPTestCommand>
{
    public VThenPTestCommandValidator()
    {
        RuleFor(c => c.SpecialNumber).NotEqual(200);
    }
}
