using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Exceptions;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Queries.Validation;

public class QueryValidatorExecutor : IQueryValidatorExecutor
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
            var handler = _factory.CreateValidator<TQuery>();
            return await handler.ValidateAsync(query);
        }
        catch (CommandHandlerNotImplementedException e)
        {
            return await Task.FromResult(Result.ErrorResult(CommandErrors.CommandHandlerNotImplemented));
        }
    }

    public async Task<Result> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IValidatingQuery, IQuery<TResult>
    {
        return await ExecuteAsync<TQuery>(query);
    }
}