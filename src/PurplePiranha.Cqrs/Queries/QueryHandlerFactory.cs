using System;

namespace PurplePiranha.Cqrs.Queries;

/// <summary>
/// Determines the correct query handler for the given query type
/// </summary>
/// <seealso cref="PurplePiranha.Cqrs.Queries.IQueryHandlerFactory" />
public class QueryHandlerFactory : IQueryHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public QueryHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Creates the correct handler for the type of query.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <returns></returns>
    public IQueryHandler<TQuery, TResult> CreateHandler<TQuery, TResult>() where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));

        if (handler is null)
            throw QueryHandlerNotImplementedException.Create<TQuery, TResult>();

        return (IQueryHandler<TQuery, TResult>)handler;
    }
}