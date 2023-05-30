using PurplePiranha.Cqrs.Exceptions;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Queries.Validation;
using PurplePiranha.FluentResults.Errors;
using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Validation.Queries
{
    public class QueryExecutorWithValidation : QueryExecutor
    {
        private readonly IQueryHandlerFactory _queryHandlerFactory;
        private readonly IQueryValidatorExecutor _queryValidatorExecutor;
        private static readonly MethodInfo _executeAsyncMethod = typeof(QueryExecutorWithValidation).GetMethod(nameof(ExecuteValidatorAsync), BindingFlags.NonPublic | BindingFlags.Instance);

        public QueryExecutorWithValidation(IQueryHandlerFactory queryHandlerFactory, IQueryValidatorExecutor queryValidatorExecutor) : base(queryHandlerFactory)
        {
            _queryHandlerFactory = queryHandlerFactory;
            _queryValidatorExecutor = queryValidatorExecutor;
        }

        protected override async Task<Result<TResult>> ExecuteQueryAsync<TQuery, TResult>(TQuery query)
        {
            Result<TResult> result = Result.ErrorResult<TResult>(Error.None);

            bool validationSuccess = false;

            var validationResult = (query is IValidatingQuery) ? await CallExecuteValidatorAsync<TQuery, TResult>(query) : Result.SuccessResult();

            validationResult
                .OnError(e => { 
                    result = Result.ErrorResult<TResult>(e);
                })
                .OnValidationFailure(v => { 
                    result = Result.ValidationFailureResult<TResult>(v);
                })
                .OnSuccess(async () => {
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

        private async Task<Result> CallExecuteValidatorAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            Result result;

            if (query == null) return default;
            try
            {
                var queryType = query.GetType();
                var resultType = typeof(TResult);
                result = await (Task<Result>)_executeAsyncMethod.MakeGenericMethod(queryType).Invoke(this, new object[] { query });
            }
            catch (TargetInvocationException ex)
            {
                result = HandleException(ex);
            }

            return result;
        }

        protected virtual async Task<Result> ExecuteValidatorAsync<TQuery>(TQuery query) where TQuery : IValidatingQuery
        {
            return await _queryValidatorExecutor.ExecuteAsync(query);
        }

        private Result HandleException(TargetInvocationException ex)
        {
            var info = ExceptionDispatchInfo.Capture(ex.InnerException);
            info.Throw();

            // compiler requires assignment
            return default;
        }
    }
}
