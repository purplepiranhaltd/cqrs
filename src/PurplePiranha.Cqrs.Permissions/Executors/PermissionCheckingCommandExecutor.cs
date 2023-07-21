using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Permissions.Factories;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Executors;

public class PermissionCheckingCommandExecutor : ICommandExecutor
{
    private readonly ICommandExecutor _commandExecutor;
    private readonly IPermissionCheckerExecutor _permissionCheckerExecutor;
    private readonly NotAuthorisedFailureFactory _notAuthorisedFailureFactory;

    public PermissionCheckingCommandExecutor(
        ICommandExecutor commandExecutor, 
        IPermissionCheckerExecutor permissionCheckerExecutor,
        NotAuthorisedFailureFactory notAuthorisedFailureFactory
        )
    {
        _commandExecutor = commandExecutor;
        _permissionCheckerExecutor = permissionCheckerExecutor;
        _notAuthorisedFailureFactory = notAuthorisedFailureFactory;
    }
    public async Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        return await PerformExecutionAsync<TCommand>(command);
    }

    public async Task<Result<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command)
    {
        return await CallPerformExecuteTAsync<TResult>(command);
    }

    private async Task<Result<TResult>> CallPerformExecuteTAsync<TResult>(ICommand<TResult> command)
    {
        var commandType = command.GetType();
        var resultType = typeof(TResult);

        try
        {
            var method = PerformExecutionTAsyncMethod.MakeGenericMethod(commandType, resultType);

#nullable disable
            return await (Task<Result<TResult>>)method.Invoke(this, new object[] { command });
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

    private async Task<bool> CallPerformPermissionCheckingAsync<TCommand>(TCommand command)
    {
        var commandType = command.GetType();

        try
        {
            var method = PerformPermissionCheckingAsyncMethod.MakeGenericMethod(commandType);

#nullable disable
            return await (Task<bool>)method.Invoke(this, new object[] { command });
#nullable disable
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

    private async Task<Result<TResult>> PerformExecutionTAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        if (command is IPermissionRequired)
        {
            var hasPermission = await CallPerformPermissionCheckingAsync(command);

            if (!hasPermission)
                return Result.FailureResult<TResult>(_notAuthorisedFailureFactory.GetNotAuthorisedFailure());
        }

        return await _commandExecutor.ExecuteAsync(command);
    }

    private async Task<Result> PerformExecutionAsync<TCommand>(TCommand command) where TCommand: ICommand
    {
        if (command is IPermissionRequired)
        {
            var hasPermission = await CallPerformPermissionCheckingAsync(command);

            if (!hasPermission)
                return Result.FailureResult(_notAuthorisedFailureFactory.GetNotAuthorisedFailure());
        }

        return await _commandExecutor.ExecuteAsync(command);
    }

    private async Task<bool> PerformPermissionCheckingAsync<TCommand>(TCommand command) where TCommand : IPermissionRequired
    {
        return await _permissionCheckerExecutor.ExecuteAsync(command);
    }

#nullable disable
    private static readonly MethodInfo PerformExecutionTAsyncMethod =
        typeof(PermissionCheckingCommandExecutor)
            .GetMethod(
                nameof(PerformExecutionTAsync),
                BindingFlags.NonPublic | BindingFlags.Instance
            );

    private static readonly MethodInfo PerformPermissionCheckingAsyncMethod =
        typeof(PermissionCheckingCommandExecutor)
            .GetMethod(
                nameof(PerformPermissionCheckingAsync),
                BindingFlags.NonPublic | BindingFlags.Instance
            );
#nullable enable
}
