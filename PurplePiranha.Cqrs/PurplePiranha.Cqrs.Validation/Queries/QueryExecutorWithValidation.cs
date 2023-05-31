﻿using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Errors;
using PurplePiranha.FluentResults.Results;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace PurplePiranha.Cqrs.Validation.Queries;

/// <summary>
/// Performs validation (if required) and then executes a query via the correct handler.
/// </summary>
/// <seealso cref="PurplePiranha.Cqrs.Queries.QueryExecutor" />
public class QueryExecutorWithValidation : QueryExecutor
{
    private readonly IQueryHandlerFactory _queryHandlerFactory;
    private readonly IQueryValidationExecutor _queryValidatorExecutor;

#nullable disable
    private static readonly MethodInfo ExecuteValidationAsyncMethod = 
        typeof(QueryExecutorWithValidation)
            .GetMethod(
                nameof(ExecuteValidationAsync), 
                BindingFlags.NonPublic | BindingFlags.Instance
            );
#nullable enable

    public QueryExecutorWithValidation(IQueryHandlerFactory queryHandlerFactory, IQueryValidationExecutor queryValidatorExecutor) : base(queryHandlerFactory)
    {
        _queryHandlerFactory = queryHandlerFactory;
        _queryValidatorExecutor = queryValidatorExecutor;
    }

    /// <summary>
    /// Executes the query.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    protected override async Task<Result<TResult>> ExecuteQueryAsync<TQuery, TResult>(TQuery query)
    {
        Result<TResult> result = Result.ErrorResult<TResult>(Error.None);

        var validationResult = (query is IValidatingQuery) ? await CallExecuteValidatorAsync<TQuery, TResult>(query) : Result.SuccessResult();

        validationResult
            .OnError(e =>
            {
                result = Result.ErrorResult<TResult>(e);
            })
            .OnValidationFailure(v =>
            {
                result = Result.ValidationFailureResult<TResult>(v);
            })
            .OnSuccess(async () =>
            {
                try
                {
                    var handler = _queryHandlerFactory.CreateHandler<TQuery, TResult>();
                    result = await handler.ExecuteAsync(query);
                }
                catch (CommandHandlerNotImplementedException e)
                {
                    result = await Task.FromResult(Result.ErrorResult<TResult>(QueryErrors.QueryHandlerNotImplemented));
                }
            });

        return result;
    }

    /// <summary>
    /// Performs the validation.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    private async Task<Result> CallExecuteValidatorAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        // We make a dynamic call to the generic method via reflection,
        // otherwise the return type would have to be specified on every call

        var queryType = query.GetType();
        var resultType = typeof(TResult);

        try
        {
            var method = ExecuteValidationAsyncMethod.MakeGenericMethod(queryType);

#nullable disable
            return await (Task<Result>)method.Invoke(this, new object[] { query });
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

    /// <summary>
    /// Performs the validation
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    protected virtual async Task<Result> ExecuteValidationAsync<TQuery>(TQuery query) where TQuery : IValidatingQuery
    {
        return await _queryValidatorExecutor.ExecuteAsync(query);
    }
}