using PurplePiranha.Cqrs.Commands;

namespace PurplePiranha.Cqrs.Tests.TestClasses.Commands;

public record TestCommandWithoutHandler() : ICommand
{
}
