using FluentValidation;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;
using PurplePiranha.FluentResults.Validation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands
{
    public class TestValidatingCommandValidator : AbstractValidator<TestValidatingCommand>
    {
        public TestValidatingCommandValidator()
        {
            RuleFor(c => c.IMustNotBe100).NotEqual(100);
        }
    }
}
