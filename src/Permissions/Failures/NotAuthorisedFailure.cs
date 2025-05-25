using PurplePiranha.FluentResults.FailureTypes;

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
