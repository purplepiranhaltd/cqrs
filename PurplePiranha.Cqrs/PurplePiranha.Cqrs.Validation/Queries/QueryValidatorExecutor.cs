using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Queries;

public class QueryValidatorExecutor : IQueryValidationExecutor
{
    private readonly IQueryValidatorFactory _factory;

    public QueryValidatorExecutor(IQueryValidatorFactory factory)
    {
        _factory = factory;
    }

    //TODO: Probably move this into the other method, but it's here to remind me when we do commands!
    public async Task<Result> ExecuteAsync<TQuery>(TQuery query) where TQuery : IValidatingQuery
    {
        try
        {
            var handler = _factory.CreateValidationHandler<TQuery>();
            return await handler.ValidateAsync(query);
        }
        catch (CommandHandlerNotImplementedException e)
        {
            return await Task.FromResult(Result.ErrorResult(CommandErrors.CommandHandlerNotImplemented));
        }
    }

    public async Task<Result> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IValidatingQuery, IQuery<TResult>
    {
        return await ExecuteAsync(query);
    }
}