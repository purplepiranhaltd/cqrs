using PurplePiranha.FluentResults.FailureTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Failures;

public class NotAuthorisedFailure : Failure
{
    public NotAuthorisedFailure() : 
        base(
            $"PurplePiranha.Cqrs.Permissions.{ nameof(NotAuthorisedFailure) }", 
            "Not authorised")
    {
    }
}
