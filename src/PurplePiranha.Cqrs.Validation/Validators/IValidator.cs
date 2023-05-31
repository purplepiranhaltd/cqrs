using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Validators;

/// <summary>
/// Handles validation for a specific query type
/// </summary>
/// <typeparam name="T">The type of the query.</typeparam>
public interface IValidator<T> where T : IValidationRequired
{
    /// <summary>
    /// Validates the query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Validation failures</returns>
    Task<Result> ValidateAsync(T query);
}