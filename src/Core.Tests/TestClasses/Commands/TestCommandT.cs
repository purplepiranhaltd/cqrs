using PurplePiranha.Cqrs.Core.Commands;

namespace PurplePiranha.Cqrs.Core.Tests.TestClasses.Commands;

public record TestCommandT() : ICommand<int>
{
}