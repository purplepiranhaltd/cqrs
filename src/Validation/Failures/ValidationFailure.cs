using FluentValidation.Results;
using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.FluentResults.FailureTypes;

namespace PurplePiranha.Cqrs.Validation.Failures;

public class ValidationFailure : Failure
{
    protected internal ValidationFailure(string message, ValidationResult validationResult) : base($"PurplePiranha.Cqrs.Validation.{ nameof(ValidationFailure) }", message, validationResult)
    {
    }

#nullable disable
    public ValidationResult ValidationResult => (ValidationResult)base.Obj;
#nullable enable

    public static ValidationFailure CreateForCommand<TCommand>(ValidationResult validationResult) where TCommand : ICommand
    {
        var message = $"Validation failed on command '{ nameof(TCommand) }'.";
        return new ValidationFailure(message, validationResult);
    }

    public static ValidationFailure CreateForCommand<TCommand, TResult>(ValidationResult validationResult) where TCommand : ICommand<TResult>
    {
        var message = $"Validation failed on command '{nameof(TCommand)}'.";
        return new ValidationFailure(message, validationResult);
    }

    public static ValidationFailure CreateForQuery<TQuery, TResult>(ValidationResult validationResult) where TQuery : IQuery<TResult>
    {
        var message = $"Validation failed on query '{nameof(TQuery)}'.";
        return new ValidationFailure(message, validationResult);
    }
}
