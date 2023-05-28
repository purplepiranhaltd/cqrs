using PurplePiranha.Cqrs.Errors;
using PurplePiranha.Cqrs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Commands
{
    internal class NotImplementedCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> ExecuteAsync(TCommand command)
        {
            return await Task.FromResult(Result.ErrorResult(Error.CommandHandlerNotImplemented));
        }
    }

    internal class NotImplementedCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        public async Task<Result<TResult>> ExecuteAsync(TCommand command)
        {
            return await Task.FromResult(Result.ErrorResult<TResult>(Error.CommandHandlerNotImplemented));
        }
    }
}
