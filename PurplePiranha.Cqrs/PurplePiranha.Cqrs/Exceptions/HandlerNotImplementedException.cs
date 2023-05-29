using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Exceptions
{
    public class HandlerNotImplementedException : Exception
    {
        public HandlerNotImplementedException()
        {
        }

        public HandlerNotImplementedException(string message)
            : base(message)
        {
        }

        public HandlerNotImplementedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
