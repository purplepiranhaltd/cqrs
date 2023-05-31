using PurplePiranha.FluentResults.Errors;

namespace PurplePiranha.Cqrs.Commands;

/// <summary>
/// Errors relating to Commands
/// </summary>
public static class CommandErrors
{
    public static readonly Error CommandHandlerNotImplemented = new($"{nameof(Error)}.{nameof(CommandHandlerNotImplemented)}", "No command handler was found to process this command");
}