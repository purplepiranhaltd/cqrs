namespace PurplePiranha.Cqrs.Commands;

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