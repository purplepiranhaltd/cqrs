using PurplePiranha.Cqrs.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands
{
    public static class CommandErrors
    {
        public static readonly Error CommandHandlerNotImplemented = new($"{nameof(Error)}.{nameof(CommandHandlerNotImplemented)}", "No command handler was found to process this command");
    }
}
