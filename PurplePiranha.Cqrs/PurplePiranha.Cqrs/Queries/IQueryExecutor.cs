using PurplePiranha.FluentResults.Results;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Queries;

public interface IQueryExecutor
{
    Task<Result<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query);
}
