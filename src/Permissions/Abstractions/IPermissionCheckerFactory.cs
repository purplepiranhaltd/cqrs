using PurplePiranha.Cqrs.Permissions.Decorators;

namespace PurplePiranha.Cqrs.Permissions.Abstractions;

/// <summary>
/// Determines the correct permission checker for the given type
/// </summary>
public interface IPermissionCheckerFactory
{
    /// <summary>
    /// Creates the correct validation handler for the type of query.
    /// </summary>
    /// <typeparam name="T">The type of the query.</typeparam>
    /// <returns></returns>
    IPermissionChecker<T> CreatePermissionChecker<T>() where T : IPermissionRequired;
}
