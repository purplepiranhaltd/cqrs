using PurplePiranha.FluentResults.Results;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Queries;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<Result<TResult>> ExecuteAsync(TQuery query);
}