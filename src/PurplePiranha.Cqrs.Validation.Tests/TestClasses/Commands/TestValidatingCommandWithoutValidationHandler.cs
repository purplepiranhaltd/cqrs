using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Validation.Validators;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands
{
    public record TestValidatingCommandWithoutValidationHandler(int IMustNotBe100) : ICommand, IValidationRequired
    {
    }
}
