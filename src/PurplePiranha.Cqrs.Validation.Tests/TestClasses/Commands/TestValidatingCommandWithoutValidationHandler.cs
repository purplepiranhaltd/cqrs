using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands
{
    public record TestValidatingCommandWithoutValidationHandler(int IMustNotBe100) : ICommand, IValidationRequired
    {
    }
}
