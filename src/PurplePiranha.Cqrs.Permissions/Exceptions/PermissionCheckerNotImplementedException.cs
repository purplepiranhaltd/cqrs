using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Exceptions
{
    public class PermissionCheckerNotImplementedException : Exception
    {
        public PermissionCheckerNotImplementedException()
        {
        }

        public PermissionCheckerNotImplementedException(string message)
            : base(message)
        {
        }

        public PermissionCheckerNotImplementedException(string message, Exception inner)
        : base(message, inner)
        {
        }

        public PermissionCheckerNotImplementedException(Type type) : base($"Permission checker for '{type.Name}' has not been implemented.")
        {
        }

        public static PermissionCheckerNotImplementedException Create<T>()
        {
            return new PermissionCheckerNotImplementedException(typeof(T));
        }
    }
}
