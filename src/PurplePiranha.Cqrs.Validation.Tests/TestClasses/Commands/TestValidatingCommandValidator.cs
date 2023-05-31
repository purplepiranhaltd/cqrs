using PurplePiranha.Cqrs.Validation.Validators;
using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands
{
    public class TestValidatingCommandValidator : IValidator<TestValidatingCommand>
    {
        public async Task<Result> ValidateAsync(TestValidatingCommand query)
        {
            if (query.IMustNotBe100 == 100)
                return await Task.FromResult(Result.ValidationFailureResult(new string[] { "Must not be 100" }.ToList())); 

            return await Task.FromResult(Result.SuccessResult());
        }
    }
}
