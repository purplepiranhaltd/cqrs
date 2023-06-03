using FluentValidation.Results;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;
using PurplePiranha.FluentResults.Validation.Results;

namespace PurplePiranha.Cqrs.Validation.Validators;

public class ValidatorExecutor : IValidatorExecutor
{
    private readonly IValidatorFactory _factory;

    public ValidatorExecutor(IValidatorFactory factory)
    {
        _factory = factory;
    }

    public async Task<ValidationResult> ExecuteAsync<TQuery>(TQuery query) where TQuery : IValidationRequired
    {
        try
        {
            var handler = _factory.CreateValidator<TQuery>();
            return await handler.ValidateAsync(query);
        }
        catch (CommandHandlerNotImplementedException e)
        {
            //TODO: Logging
            throw;
        }
    }
}