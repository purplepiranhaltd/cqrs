using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Abstractions;

/// <summary>
/// Performs permission checking via the correct permission checker
/// </summary>
public interface IPermissionCheckerExecutor
{
    /// <summary>
    /// Executes the permission checking.
    /// </summary>
    /// <typeparam name="T">The type of the query.</typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    Task<bool> ExecuteAsync<T>(T query, CancellationToken cancellationToken = default) where T : IPermissionRequired;
}
