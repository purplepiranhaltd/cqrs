using PurplePiranha.FluentResults.Errors;

namespace PurplePiranha.Cqrs.Queries;

public static class QueryErrors
{
    public static readonly Error QueryHandlerNotImplemented = new($"{nameof(Error)}.{nameof(QueryHandlerNotImplemented)}", "No query handler was found to process this command");
}