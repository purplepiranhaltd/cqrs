using System;

namespace PurplePiranha.Cqrs.Exceptions;

public class QueryValidationHandlerNotImplementedException : Exception
{
    public QueryValidationHandlerNotImplementedException()
    {
    }

    public QueryValidationHandlerNotImplementedException(string message)
        : base(message)
    {
    }

    public QueryValidationHandlerNotImplementedException(string message, Exception inner)
    : base(message, inner)
    {
    }

    public QueryValidationHandlerNotImplementedException(Type queryType) : base($"Query validation handler for query '{ queryType.Name }' has not been implemented.")
    {
    }

    public static QueryValidationHandlerNotImplementedException Create<TQuery>()
    {
        return new QueryValidationHandlerNotImplementedException(typeof(TQuery));
    }
}
