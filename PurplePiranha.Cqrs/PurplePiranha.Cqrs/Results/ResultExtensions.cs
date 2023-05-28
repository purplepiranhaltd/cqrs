using System;

namespace PurplePiranha.Cqrs.Results
{
    public static class ResultExtensions
    {
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
                action();

            return result;
        }

        public static Result OnValidationFailure(this Result result, Action action)
        {
            if (result.IsValidationFailure)
                action();

            return result;
        }

        public static Result OnError(this Result result, Action action)
        {
            if (result.IsError)
                action();

            return result;
        }
    }
}
