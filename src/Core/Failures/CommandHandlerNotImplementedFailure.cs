using PurplePiranha.FluentResults.FailureTypes;

namespace PurplePiranha.Cqrs.Core.Failures;

public class CommandHandlerNotImplementedFailure : Failure
{
    public CommandHandlerNotImplementedFailure(string commandName) : base($"PurplePiranha.Cqrs.{ nameof(CommandHandlerNotImplementedFailure) }", $"No command handler was found to process the command { commandName }")
    {
    }
}
