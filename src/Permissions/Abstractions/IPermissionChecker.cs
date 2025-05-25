namespace PurplePiranha.Cqrs.Permissions.Abstractions;

public interface IPermissionChecker<T>
{
    Task<bool> HasPermissionAsync(T obj);

    /// <summary>
    /// Initialises the permission checker.
    /// </summary>
    /// <returns></returns>
    Task InitialiseAsync();
}
