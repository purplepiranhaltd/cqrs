using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Commands.CommandWithoutReturnType
{
    /// <summary>
    /// A command handler that takes a simple command. 
    /// Returns a successful result if both inputs are equal.
    /// Returns a validation error if either input is negative.
    /// </summary>
    /// <seealso cref="ICommandHandler&lt;ACommandWithoutReturnType.SimpleCommand&gt;" />
    public class ACommandWithoutReturnTypeHandler : ICommandHandler<ACommandWithoutReturnType>
    {
        public async Task<Result> ExecuteAsync(ACommandWithoutReturnType command)
        {
            var validationFailures = new List<string>();

            if (command.A < 0)
                validationFailures.Add("A Negative");

            if (command.B < 0)
                validationFailures.Add("B Negative");

            if (validationFailures.Count > 0)
                return await Task.FromResult(Result.ValidationFailureResult(validationFailures));

            if (command.A.Equals(command.B))
                return await Task.FromResult(Result.SuccessResult());

            return await Task.FromResult(Result.ErrorResult(ACommandWithoutReturnTypeErrors.BothInputsMustBeEqual));
        }
    }
}
