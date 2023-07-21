using PurplePiranha.FluentResults.FailureTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Failures;

public class QueryHandlerNotImplementedFailure : Failure
{
    public QueryHandlerNotImplementedFailure(string queryName) : base($"PurplePiranha.Cqrs.{ nameof(QueryHandlerNotImplementedFailure) }", $"No query handler was found to process the command { queryName }")
    {
    }
}
