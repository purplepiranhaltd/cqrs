using PurplePiranha.FluentResults.Results;
using System.Threading;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Core.Queries;

/// <summary>
/// Handles the execution of an individual query
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    /// <summary>
    /// Executes the query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    Task<Result<TResult>> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
}