using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Validation.Queries;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;

public record TestValidatingQueryWithoutValidationHandler(int A) : IQuery<int>, IValidatingQuery
{
}