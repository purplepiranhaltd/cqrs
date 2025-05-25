using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.Cqrs.Validation.Validators;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands;

public record TestValidatingCommand(int IMustNotBe100) : ICommand, IValidationRequired
{
}
