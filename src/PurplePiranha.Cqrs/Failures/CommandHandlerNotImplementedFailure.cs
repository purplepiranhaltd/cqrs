using PurplePiranha.FluentResults.FailureTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Failures;

public class CommandHandlerNotImplementedFailure : Failure
{
    public CommandHandlerNotImplementedFailure(string commandName) : base($"PurplePiranha.Cqrs.{ nameof(CommandHandlerNotImplementedFailure) }", $"No command handler was found to process the command { commandName }")
    {
    }
}
