using PurplePiranha.Cqrs.Commands;
using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Commands
{
    public class TestValidatingCommandHandler : ICommandHandler<TestValidatingCommand>
    {
        public async Task<Result> ExecuteAsync(TestValidatingCommand command)
        {
            return await Task.FromResult(Result.SuccessResult());
        }
    }
}
