namespace PurplePiranha.Cqrs.Queries.Validation;

public interface IQueryValidatorFactory
{
    IQueryValidator<TQuery> CreateValidator<TQuery>() where TQuery : IValidatingQuery;
}
