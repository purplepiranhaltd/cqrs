using FluentValidation;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.ValidationThenPermissions.CommandT;

public class VThenPTestCommandTValidator : AbstractValidator<VThenPTestCommandT>
{
    public VThenPTestCommandTValidator()
    {
        RuleFor(c => c.SpecialNumber).NotEqual(200);
    }
}
