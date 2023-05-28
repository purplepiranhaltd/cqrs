using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests
{
    /// <summary>
    /// A command handler that takes a simple command. 
    /// Returns a successful result if both inputs are equal.
    /// Returns a validation error if either input is negative.
    /// </summary>
    /// <seealso cref="PurplePiranha.Cqrs.Commands.ICommandHandler&lt;PurplePiranha.Cqrs.Tests.SimpleCommand&gt;" />
    public class SimpleCommandHandler : ICommandHandler<SimpleCommand>
    {
        public async Task<Result> ExecuteAsync(SimpleCommand command)
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

            return await Task.FromResult(Result.ErrorResult(SimpleCommandErrors.BothInputsMustBeEqual));
        }
    }
}
