using FluentValidation;

namespace PurplePiranha.Cqrs.Validation.Validators;

/// <summary>
/// Determines the correct validator for the given query type
/// </summary>
public interface IValidatorFactory
{
    /// <summary>
    /// Creates the correct validation handler for the type of query.
    /// </summary>
    /// <typeparam name="T">The type of the query.</typeparam>
    /// <returns></returns>
    IValidator<T> CreateValidator<T>() where T : IValidationRequired;
}