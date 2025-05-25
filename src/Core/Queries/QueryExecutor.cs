using Microsoft.Extensions.Logging;
using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.Cqrs.Core.Failures;
using PurplePiranha.FluentResults.Results;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Core.Queries;

/// <summary>
/// Executes a query via the correct handler.
/// </summary>
/// <seealso cref="IQueryExecutor" />
public class QueryExecutor : IQueryExecutor
{
    private readonly IQueryHandlerFactory _queryHandlerFactory;
    private readonly ILogger<QueryExecutor> _logger;

#nullable disable
    private static MethodInfo ExecuteQueryAsyncMethod => 
        typeof(QueryExecutor)
            .GetMethod(
                nameof(ExecuteQueryAsync), 
                BindingFlags.NonPublic | BindingFlags.Instance
            );
#nullable enable

    public QueryExecutor(IQueryHandlerFactory queryHandlerFactory, ILogger<QueryExecutor> logger)
    {
        _queryHandlerFactory = queryHandlerFactory;
        _logger = logger;
    }

    public async Task<Result<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query)
    {
        _logger.LogInformation($"Executing query: {query.GetType().Name}");

        var queryType = query.GetType();
        var resultType = typeof(TResult);

        try
        {
            var method = ExecuteQueryAsyncMethod.MakeGenericMethod(queryType, resultType);

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

    protected virtual async Task<Result<TResult>> ExecuteQueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        try
        {
            var handler = _queryHandlerFactory.CreateHandler<TQuery, TResult>();

            return await handler.ExecuteAsync(query);
        }
        catch (CommandHandlerNotImplementedException e)
        {
            return await Task.FromResult(Result.FailureResult<TResult>(new QueryHandlerNotImplementedFailure(nameof(TQuery))));
        }
    }
}