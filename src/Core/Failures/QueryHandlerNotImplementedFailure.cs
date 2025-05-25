using PurplePiranha.FluentResults.FailureTypes;

namespace PurplePiranha.Cqrs.Core.Failures;

public class QueryHandlerNotImplementedFailure : Failure
{
    public QueryHandlerNotImplementedFailure(string queryName) : base($"PurplePiranha.Cqrs.{ nameof(QueryHandlerNotImplementedFailure) }", $"No query handler was found to process the command { queryName }")
    {
    }
}
