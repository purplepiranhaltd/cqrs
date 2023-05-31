using PurplePiranha.Cqrs.Queries;

namespace PurplePiranha.Cqrs.Tests.TestClasses.Queries;

public record TestQuery(int Id) : IQuery<string>
{
}