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

    //TODO: Probably move this into the other method, but it's here to remind me when we do commands!
    public async Task<ResultWithValidation> ExecuteAsync<TQuery>(TQuery query) where TQuery : IValidationRequired
    {
        try
        {
            var handler = _factory.CreateValidator<TQuery>();
            return await handler.ValidateAsync(query);
        }
        catch (CommandHandlerNotImplementedException e)
        {
            return await Task.FromResult(ResultWithValidation.ErrorResult(CommandErrors.CommandHandlerNotImplemented));
        }
    }

    //public async Task<Result> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IValidationRequired, IQuery<TResult>
    //{
    //    return await ExecuteAsync(query);
    //}
}