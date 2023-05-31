using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Queries;

public interface IQueryValidator<TQuery> where TQuery : IValidatingQuery
{
    /// <summary>
    /// Validates the asynchronous.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Validation failures</returns>
    Task<Result> ValidateAsync(TQuery query);
}