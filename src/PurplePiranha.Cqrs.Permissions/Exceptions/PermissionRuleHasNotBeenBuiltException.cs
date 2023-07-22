using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
