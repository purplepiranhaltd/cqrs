using PurplePiranha.Cqrs.Commands;

namespace PurplePiranha.Cqrs.Tests.TestClasses.Commands;

public record TestCommandT() : ICommand<int>
{
}