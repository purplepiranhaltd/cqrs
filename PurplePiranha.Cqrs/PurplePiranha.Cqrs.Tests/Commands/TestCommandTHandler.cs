﻿using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Results;

namespace PurplePiranha.Cqrs.Tests.Commands
{
    public class TestCommandTHandler : ICommandHandler<TestCommandT,int>
    {
        public async Task<Result<int>> ExecuteAsync(TestCommandT command)
        {
            return await Task.FromResult(Result.SuccessResult(15));
        }
    }
}
