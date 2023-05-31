namespace PurplePiranha.Cqrs.Validation.Queries;

/// <summary>
/// Determines the correct validation handler for the given query type
/// </summary>
public interface IQueryValidatorFactory
{
    /// <summary>
    /// Creates the correct validation handler for the type of query.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns></returns>
    IQueryValidationHandler<TQuery> CreateValidationHandler<TQuery>() where TQuery : IValidatingQuery;
}