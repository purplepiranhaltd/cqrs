using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Errors;
using PurplePiranha.Cqrs.Exceptions;
using PurplePiranha.Cqrs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IQueryHandlerFactory _queryHandlerFactory;
        private static readonly MethodInfo _executeAsyncMethod = typeof(QueryExecutor).GetMethod(nameof(ExecuteQueryAsync), BindingFlags.NonPublic | BindingFlags.Instance);

        public QueryExecutor(IQueryHandlerFactory queryHandlerFactory)
        {
            _queryHandlerFactory = queryHandlerFactory;
        }

        public async Task<Result<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            Result<TResult> result;

            if (query == null) return default;
            try
            {
                var queryType = query.GetType();
                var resultType = typeof(TResult);
                result = await(Task<Result<TResult>>)_executeAsyncMethod.MakeGenericMethod(queryType, resultType).Invoke(this, new object[] { query });
            }
            catch (TargetInvocationException ex)
            {
                result = HandleException<TResult>(ex);
            }

            return result;
        }

        private async Task<Result<TResult>> ExecuteQueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            try
            {
                var handler = _queryHandlerFactory.CreateHandler<TQuery, TResult>();
                return await handler.ExecuteAsync(query);
            }
            catch (HandlerNotImplementedException e)
            {
                return await Task.FromResult(Result.ErrorResult<TResult>(QueryErrors.QueryHandlerNotImplemented));
            }
        }

        private TResult HandleException<TResult>(TargetInvocationException ex)
        {
            var info = ExceptionDispatchInfo.Capture(ex.InnerException);
            info.Throw();

            // compiler requires assignment
            return default;
        }
    }
}
