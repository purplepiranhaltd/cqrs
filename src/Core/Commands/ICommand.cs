namespace PurplePiranha.Cqrs.Core.Commands;

/// <summary>
/// Represents a Command
/// </summary>
public interface ICommand
{
}

/// <summary>
/// Represents a Command with a Result
/// </summary>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface ICommand<TResult>
{
}