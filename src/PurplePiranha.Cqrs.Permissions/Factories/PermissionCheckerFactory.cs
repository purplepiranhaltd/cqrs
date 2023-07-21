using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Permissions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Factories;

public class PermissionCheckerFactory : IPermissionCheckerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PermissionCheckerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IPermissionChecker<T> CreatePermissionChecker<T>() where T : IPermissionRequired
    {
        var handler = _serviceProvider.GetService(typeof(IPermissionChecker<T>));

        if (handler is null)
            throw PermissionCheckerNotImplementedException.Create<T>();

        return (IPermissionChecker<T>)handler;
    }
}
