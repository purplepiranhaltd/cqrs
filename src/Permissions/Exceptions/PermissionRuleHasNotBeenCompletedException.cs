namespace PurplePiranha.Cqrs.Permissions.Exceptions;

public class PermissionRuleHasNotBeenCompletedException : Exception
{
    public PermissionRuleHasNotBeenCompletedException()
    {
    }

    public PermissionRuleHasNotBeenCompletedException(string message)
        : base(message)
    {
    }

    public PermissionRuleHasNotBeenCompletedException(string message, Exception inner)
    : base(message, inner)
    {
    }
}
