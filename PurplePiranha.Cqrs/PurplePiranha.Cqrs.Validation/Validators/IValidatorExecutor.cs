﻿using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Validators;

/// <summary>
/// Performs validation on a query via the correct validator
/// </summary>
public interface IValidatorExecutor
{
    /// <summary>
    /// Executes the validation.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    Task<Result> ExecuteAsync<TQuery>(TQuery query) where TQuery : IValidationRequired;

    ///// <summary>
    ///// Executes the validation.
    ///// </summary>
    ///// <typeparam name="TQuery">The type of the query.</typeparam>
    ///// <typeparam name="TResult">The type of the result.</typeparam>
    ///// <param name="query">The query.</param>
    ///// <returns></returns>
    //Task<Result> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>, IValidationRequired;
}