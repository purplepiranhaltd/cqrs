using PurplePiranha.FluentResults.FailureTypes;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses;

public class CustomNotAuthorisedFailure : Failure
{
    public CustomNotAuthorisedFailure() : base("TEST", "")
    {
    }
}
