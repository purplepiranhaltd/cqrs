using System;

namespace PurplePiranha.Cqrs.Queries;

public class QueryHandlerNotImplementedException : Exception
{
    public QueryHandlerNotImplementedException()
    {
    }

    public QueryHandlerNotImplementedException(string message)
        : base(message)
    {
    }

    public QueryHandlerNotImplementedException(string message, Exception inner)
    : base(message, inner)
    {
    }

    public QueryHandlerNotImplementedException(Type queryType, Type resultType) : base($"Query handler for query '{queryType.Name}' with result type '{resultType.Name}' has not been implemented.")
    {
    }

    public static QueryHandlerNotImplementedException Create<TQuery, TResult>()
    {
        return new QueryHandlerNotImplementedException(typeof(TQuery), typeof(TResult));
    }
}