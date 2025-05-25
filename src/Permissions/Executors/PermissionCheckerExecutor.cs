using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Permissions.Exceptions;

namespace PurplePiranha.Cqrs.Permissions.Executors;

public class PermissionCheckerExecutor : IPermissionCheckerExecutor
{
    private readonly IPermissionCheckerFactory _factory;

    public PermissionCheckerExecutor(IPermissionCheckerFactory factory) 
    {
        _factory = factory;
    }
    public async Task<bool> ExecuteAsync<T>(T obj, CancellationToken cancellationToken = default) where T : IPermissionRequired
    {
        //TODO: Should be be using the CancellationToken throughout the permission checking process?
        try
        {
            var handler = _factory.CreatePermissionChecker<T>();
            await handler.InitialiseAsync();
            return await handler.HasPermissionAsync(obj);
        }
        catch (PermissionCheckerNotImplementedException e)
        {
            //TODO: Do we want to iomplement some kind of logging for this or should the implementing application be dealing with it?
            throw;
        }
    }
}
