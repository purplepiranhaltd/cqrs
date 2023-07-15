using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Queries
{
    /// <summary>
    /// Executes a query via the correct handler and performs validation beforehand if required.
    /// </summary>
    public interface IValidatingQueryExecutor : IQueryExecutor
    {
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        new Task<Result<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}
