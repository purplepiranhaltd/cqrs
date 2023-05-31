using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Queries.Validation;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;

public record TestValidatingQuery(int A) : IQuery<int>, IValidatingQuery
{
}