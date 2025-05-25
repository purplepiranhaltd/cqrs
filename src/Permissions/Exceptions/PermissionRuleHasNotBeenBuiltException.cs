namespace PurplePiranha.Cqrs.Permissions.Exceptions;

public class PermissionRuleHasNotBeenBuiltException : Exception
{
    public PermissionRuleHasNotBeenBuiltException()
    {
    }

    public PermissionRuleHasNotBeenBuiltException(string message)
        : base(message)
    {
    }

    public PermissionRuleHasNotBeenBuiltException(string message, Exception inner)
    : base(message, inner)
    {
    }
}
