using PurplePiranha.FluentResults.Results;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Core.Queries;

/// <summary>
/// Executes a query via the correct handler.
/// </summary>
public interface IQueryExecutor
{
    /// <summary>
    /// Executes the query.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    Task<Result<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query);
}