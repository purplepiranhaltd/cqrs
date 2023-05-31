using PurplePiranha.Cqrs.Queries;

namespace PurplePiranha.Cqrs.Tests.TestClasses.Queries;

public record TestQueryWithoutHandler(int Id) : IQuery<string>
{
}