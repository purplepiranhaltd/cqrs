using PurplePiranha.Cqrs.Commands;

namespace PurplePiranha.Cqrs.Tests.Commands
{
    public record TestCommandT() : ICommand<int>
    {
    }
}
