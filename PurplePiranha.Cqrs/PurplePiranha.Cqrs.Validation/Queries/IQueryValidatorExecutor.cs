using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Queries;

public interface IQueryValidatorExecutor
{
    Task<Result> ExecuteAsync<TQuery>(TQuery query) where TQuery : IValidatingQuery;

    Task<Result> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>, IValidatingQuery;
}