using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Decorators
{
    /// <summary>
    /// Decorator for a query or command that requires permission to be checked.
    /// </summary>
    public interface IPermissionRequired
    {
    }
}
