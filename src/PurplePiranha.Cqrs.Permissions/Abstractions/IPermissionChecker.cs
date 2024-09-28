using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
