using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Queries;

/// <summary>
/// Handles validation for a specific query type
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
public interface IQueryValidationHandler<TQuery> where TQuery : IValidatingQuery
{
    /// <summary>
    /// Validates the query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Validation failures</returns>
    Task<Result> ValidateAsync(TQuery query);
}