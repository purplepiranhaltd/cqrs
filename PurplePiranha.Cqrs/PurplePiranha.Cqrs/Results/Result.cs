using PurplePiranha.Cqrs.Errors;
using System;
using System.Collections.Generic;

namespace PurplePiranha.Cqrs.Results
{
    public class Result
    {
        protected internal Result()
        {
            // success
            ResultType = ResultType.Success;
            Error = Error.None;
            ValidationErrors = new List<string>();

        }

        protected internal Result(Error error)
        {
            // error
            ResultType = ResultType.Error;
            Error = error;
            ValidationErrors = new List<string>();
        }

        protected internal Result(IEnumerable<string> validationErrors)
        {
            // validation error(s)
            ResultType = ResultType.ValidationFailure;
            Error = Error.None;
            ValidationErrors = validationErrors;
        }

        public static Result SuccessResult() => new();
        public static Result<TValue> SuccessResult<TValue>(TValue value) => new Result<TValue>(value);
        public static Result ErrorResult(Error error) => new(error);
        public static Result<TValue> ErrorResult<TValue>(Error error) => new(default, error);
        public static Result ValidationFailureResult(IEnumerable<string> validationErrors) => new(validationErrors);
        public static Result<TValue> ValidationFailureResult<TValue>(IEnumerable<string> validationErrors) => new(default, validationErrors);

        public ResultType ResultType { get; }
        public Error Error { get; }
        public IEnumerable<string> ValidationErrors { get; }

        public bool IsSuccess => ResultType == ResultType.Success;
        public bool IsValidationFailure => ResultType == ResultType.ValidationFailure;
        public bool IsError => ResultType == ResultType.Error;

        public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? SuccessResult(value) : ErrorResult<TValue>(Error.NullValue);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        protected internal Result(TValue? value) : base() => _value = value;
        protected internal Result(TValue? value, Error error) : base(error) => _value = value;
        protected internal Result(TValue? value, IEnumerable<string> validationErrors) : base(validationErrors) => _value = value;

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }
}
