using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;
using PurplePiranha.FluentResults.Validation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Validation.Queries
{
    public class ValidatingQueryExecutor : QueryExecutor, IValidatingQueryExecutor
    {
        private readonly IValidatorExecutor _validatorExecutor;

#nullable disable
        private static readonly MethodInfo PerformExecutionAsyncMethod =
            typeof(ValidatingQueryExecutor)
                .GetMethod(
                    nameof(PerformExecutionAsync),
                    BindingFlags.NonPublic | BindingFlags.Instance
                );

        private static readonly MethodInfo PerformValidatationAsyncMethod =
            typeof(ValidatingQueryExecutor)
                .GetMethod(
                    nameof(PerformValidatationAsync),
                    BindingFlags.NonPublic | BindingFlags.Instance
                );
#nullable enable

        public ValidatingQueryExecutor(IValidatorExecutor validatorExecutor, /*IQueryExecutor queryExecutor,*/ IQueryHandlerFactory queryHandlerFactory) : base(queryHandlerFactory)
        {
            _validatorExecutor = validatorExecutor;
            //_queryExecutor = queryExecutor;
        }

        public new async Task<ResultWithValidation<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            return await CallPerformExecuteAsync<TResult>(query);
        }

        private async Task<ResultWithValidation<TResult>> CallPerformExecuteAsync<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            var resultType = typeof(TResult);

            try
            {
                var method = PerformExecutionAsyncMethod.MakeGenericMethod(queryType, resultType);

#nullable disable
                return await (Task<ResultWithValidation<TResult>>)method.Invoke(this, new object[] { query });
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

        

        private async Task<ResultWithValidation> CallPerformValidatationAsync<TQuery>(TQuery query)
        {
            var queryType = query.GetType();

            try
            {
                var method = PerformValidatationAsyncMethod.MakeGenericMethod(queryType);

#nullable disable
                return await (Task<ResultWithValidation>)method.Invoke(this, new object[] { query });
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

        private async Task<ResultWithValidation<TResult>> PerformExecutionAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var validationResult = (query is IValidationRequired) ? await CallPerformValidatationAsync(query) : ResultWithValidation.SuccessResult();

            if (!validationResult.IsSuccess)
                return validationResult;

            return await base.ExecuteAsync(query);
        }

        /// <summary>
        /// Performs the validation
        /// </summary>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private async Task<ResultWithValidation> PerformValidatationAsync<TQuery>(TQuery query) where TQuery : IValidationRequired
        {
            return await _validatorExecutor.ExecuteAsync(query);
        }
    }
}
