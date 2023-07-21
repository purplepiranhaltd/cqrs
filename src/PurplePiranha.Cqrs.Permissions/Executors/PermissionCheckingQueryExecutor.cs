using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Permissions.Factories;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Executors;

public class PermissionCheckingQueryExecutor : IQueryExecutor
{
    #region Fields
    private readonly IQueryExecutor _queryExecutor;
    private readonly IPermissionCheckerExecutor _permissionCheckerExecutor;
    private readonly NotAuthorisedFailureFactory _notAuthorisedFailureFactory;
    #endregion

    #region Ctr
    public PermissionCheckingQueryExecutor(
        IQueryExecutor queryExecutor, 
        IPermissionCheckerExecutor permissionCheckerExecutor,
        NotAuthorisedFailureFactory notAuthorisedFailureFactory
        )
    {
        _queryExecutor = queryExecutor;
        _permissionCheckerExecutor = permissionCheckerExecutor;
        _notAuthorisedFailureFactory = notAuthorisedFailureFactory;
    }
    #endregion
    #region Methods
    public async Task<Result<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query)
    {
        return await CallPerformExecuteAsync<TResult>(query);
    }
    #endregion
    #region Helpers

    private async Task<Result<TResult>> CallPerformExecuteAsync<TResult>(IQuery<TResult> query)
    {
        var queryType = query.GetType();
        var resultType = typeof(TResult);

        try
        {
            var method = PerformExecutionAsyncMethod.MakeGenericMethod(queryType, resultType);

#nullable disable
            return await (Task<Result<TResult>>)method.Invoke(this, new object[] { query });
#nullable enable

        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException is null)
                throw;

            var info = ExceptionDispatchInfo.Capture(ex.InnerException);
            info.Throw();

            // compiler requires assignment - an exception is always thrown so we can never get here
            return default;
        }
    }

    private async Task<bool> CallPerformPermissionCheckingAsync<TQuery>(TQuery query)
    {
        var queryType = query.GetType();

        try
        {
            var method = PerformPermissionCheckingAsyncMethod.MakeGenericMethod(queryType);

#nullable disable
            return await (Task<bool>)method.Invoke(this, new object[] { query });
#nullable enable
        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException is null)
                throw;

            var info = ExceptionDispatchInfo.Capture(ex.InnerException);
            info.Throw();

            // compiler requires assignment - an exception is always thrown so we can never get here
            return default;
        }
    }

    private async Task<Result<TResult>> PerformExecutionAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        if (query is IPermissionRequired)
        {
            var hasPermission = await CallPerformPermissionCheckingAsync(query);

            if (!hasPermission)
                return Result.FailureResult(_notAuthorisedFailureFactory.GetNotAuthorisedFailure());
        }

        return await _queryExecutor.ExecuteAsync(query);
    }

    private async Task<bool> PerformPermissionCheckingAsync<TQuery>(TQuery query) where TQuery : IPermissionRequired
    {
        return await _permissionCheckerExecutor.ExecuteAsync(query);
    }
    #endregion

    #region MethodInfo
#nullable disable
    private static readonly MethodInfo PerformExecutionAsyncMethod =
        typeof(PermissionCheckingQueryExecutor)
            .GetMethod(
                nameof(PerformExecutionAsync),
                BindingFlags.NonPublic | BindingFlags.Instance
            );

    private static readonly MethodInfo PerformPermissionCheckingAsyncMethod =
        typeof(PermissionCheckingQueryExecutor)
            .GetMethod(
                nameof(PerformPermissionCheckingAsync),
                BindingFlags.NonPublic | BindingFlags.Instance
            );
#nullable enable
    #endregion
}
