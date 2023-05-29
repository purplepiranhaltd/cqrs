using PurplePiranha.Cqrs.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Queries
{
    public static class QueryErrors
    {
        public static readonly Error QueryHandlerNotImplemented = new($"{nameof(Error)}.{nameof(QueryHandlerNotImplemented)}", "No query handler was found to process this command");
    }
}
