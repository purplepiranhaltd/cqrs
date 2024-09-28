using FluentValidation.Results;

namespace PurplePiranha.Cqrs.Validation.Validators;

/// <summary>
/// Performs validation on a query via the correct validator
/// </summary>
public interface IValidatorExecutor
{
    /// <summary>
    /// Executes the validation.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    Task<ValidationResult> ExecuteAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IValidationRequired;

    ///// <summary>
    ///// Executes the validation.
    ///// </summary>
    ///// <typeparam name="TQuery">The type of the query.</typeparam>
    ///// <typeparam name="TResult">The type of the result.</typeparam>
    ///// <param name="query">The query.</param>
    ///// <returns></returns>
    //Task<Result> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>, IValidationRequired;
}