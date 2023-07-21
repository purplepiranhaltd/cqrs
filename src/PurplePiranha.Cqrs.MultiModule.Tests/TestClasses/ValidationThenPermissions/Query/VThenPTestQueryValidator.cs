using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.ValidationThenPermissions.Query;

public class VThenPTestQueryValidator : AbstractValidator<VThenPTestQuery>
{
    public VThenPTestQueryValidator()
    {
        RuleFor(c => c.SpecialNumber).NotEqual(200);
    }
}
