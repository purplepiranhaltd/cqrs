using PurplePiranha.Cqrs.Core.Queries;

namespace PurplePiranha.Cqrs.Core.Tests.TestClasses.Queries;

public record TestQueryWithoutHandler(int Id) : IQuery<string>
{
}