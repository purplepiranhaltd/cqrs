using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Permissions.Exceptions;
using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Executors;

public class PermissionCheckerExecutor : IPermissionCheckerExecutor
{
    private readonly IPermissionCheckerFactory _factory;

    public PermissionCheckerExecutor(IPermissionCheckerFactory factory) 
    {
        _factory = factory;
    }
    public async Task<bool> ExecuteAsync<T>(T obj) where T : IPermissionRequired
    {
        try
        {
            var handler = _factory.CreatePermissionChecker<T>();
            return await handler.HasPermission(obj);
        }
        catch (PermissionCheckerNotImplementedException e)
        {
            //TODO: Logging
            throw;
        }
    }
}
