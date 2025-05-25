namespace PurplePiranha.Cqrs.Core.Queries;

/// <summary>
/// Determines the correct query handler for the given query type
/// </summary>
public interface IQueryHandlerFactory
{
    /// <summary>
    /// Creates the correct handler for the type of query.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <returns></returns>
    IQueryHandler<TQuery, TResult> CreateHandler<TQuery, TResult>() where TQuery : IQuery<TResult>;
}