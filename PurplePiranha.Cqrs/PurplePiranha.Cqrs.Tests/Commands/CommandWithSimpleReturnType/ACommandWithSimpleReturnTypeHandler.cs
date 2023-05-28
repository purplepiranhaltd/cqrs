using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Results;
using PurplePiranha.Cqrs.Tests.Commands.CommandWithoutReturnType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Commands.CommandWithSimpleReturnType
{
    public class ACommandWithSimpleReturnTypeHandler : ICommandHandler<ACommandWithSimpleReturnType, int>
    {
        public async Task<Result<int>> ExecuteAsync(ACommandWithSimpleReturnType command)
        {
            var validationFailures = new List<string>();

            if (command.A < 0)
                validationFailures.Add("A Negative");

            if (command.B < 0)
                validationFailures.Add("B Negative");

            if (validationFailures.Count > 0)
                return await Task.FromResult(Result.ValidationFailureResult<int>(validationFailures));

            try
            {
                int result = checked(command.A + command.B);
                return await Task.FromResult(Result.SuccessResult<int>(result));
            }
            catch (OverflowException ex)
            {
                return await Task.FromResult(Result.ErrorResult<int>(ACommandWithSimpleReturnTypeErrors.ArithmeticOverFlow));
            }
        }
    }
}
