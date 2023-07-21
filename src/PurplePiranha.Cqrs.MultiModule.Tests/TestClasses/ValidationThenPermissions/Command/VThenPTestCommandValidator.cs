using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.ValidationThenPermissions.Command;

public class VThenPTestCommandValidator : AbstractValidator<VThenPTestCommand>
{
    public VThenPTestCommandValidator()
    {
        RuleFor(c => c.SpecialNumber).NotEqual(200);
    }
}
