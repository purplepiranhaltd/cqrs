using PurplePiranha.FluentResults.Errors;

namespace PurplePiranha.Cqrs.Commands;

public static class CommandErrors
{
    public static readonly Error CommandHandlerNotImplemented = new($"{nameof(Error)}.{nameof(CommandHandlerNotImplemented)}", "No command handler was found to process this command");
}
