using PurplePiranha.Cqrs.Core.Queries;

namespace PurplePiranha.Cqrs.Core.Tests.TestClasses.Queries;

public record TestQuery(int Id) : IQuery<string>
{
}