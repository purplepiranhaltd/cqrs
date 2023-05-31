using PurplePiranha.Cqrs.Exceptions;
using System;

namespace PurplePiranha.Cqrs.Queries;

public class QueryHandlerFactory : IQueryHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public QueryHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IQueryHandler<TQuery, TResult> CreateHandler<TQuery, TResult>() where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));

        if (handler is null)
            throw QueryHandlerNotImplementedException.Create<TQuery, TResult>();

        return (IQueryHandler<TQuery, TResult>)handler;
    }
}