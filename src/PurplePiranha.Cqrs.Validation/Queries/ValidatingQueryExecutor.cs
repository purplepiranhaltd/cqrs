﻿using FluentValidation.Results;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Validation.Failures;
using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;
using System.Reflection;
using System.Runtime.ExceptionServices;
using ValidationFailure = PurplePiranha.Cqrs.Validation.Failures.ValidationFailure;

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

        public new async Task<Result<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            return await CallPerformExecuteAsync<TResult>(query);
        }

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

        

        private async Task<ValidationResult> CallPerformValidatationAsync<TQuery>(TQuery query)
        {
            var queryType = query.GetType();

            try
            {
                var method = PerformValidatationAsyncMethod.MakeGenericMethod(queryType);

#nullable disable
                return await (Task<ValidationResult>)method.Invoke(this, new object[] { query });
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
            if (query is IValidationRequired)
            {
                var validationResult = await CallPerformValidatationAsync(query);

                if (!validationResult.IsValid)
                    return Result.FailureResult(ValidationFailure.CreateForQuery<TQuery, TResult>(validationResult));
            }          

            return await base.ExecuteAsync(query);
        }

        /// <summary>
        /// Performs the validation
        /// </summary>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private async Task<ValidationResult> PerformValidatationAsync<TQuery>(TQuery query) where TQuery : IValidationRequired
        {
            return await _validatorExecutor.ExecuteAsync(query);
        }
    }
}
