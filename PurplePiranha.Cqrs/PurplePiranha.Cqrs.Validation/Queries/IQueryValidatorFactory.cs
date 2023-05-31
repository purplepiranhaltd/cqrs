namespace PurplePiranha.Cqrs.Validation.Queries;

public interface IQueryValidatorFactory
{
    IQueryValidator<TQuery> CreateValidator<TQuery>() where TQuery : IValidatingQuery;
}