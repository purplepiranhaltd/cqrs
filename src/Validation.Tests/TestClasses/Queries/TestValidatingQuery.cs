using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.Cqrs.Validation.Validators;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;

public record TestValidatingQuery(int A) : IQuery<int>, IValidationRequired
{
}