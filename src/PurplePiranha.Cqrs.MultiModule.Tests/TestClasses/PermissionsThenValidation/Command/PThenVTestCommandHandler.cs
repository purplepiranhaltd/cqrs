using PurplePiranha.Cqrs.Commands;
using PurplePiranha.FluentResults.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.PermissionsThenValidation.Command;

public class PThenVTestCommandHandler : ICommandHandler<PThenVTestCommand>
{
    public Task<Result> ExecuteAsync(PThenVTestCommand command)
    {
        // Ensure that query isn't executed before permission checking
        Assert.That(command.SpecialNumber, Is.Not.EqualTo(100));

        // Ensure that query isn't executed before validation check
        Assert.That(command.SpecialNumber, Is.Not.EqualTo(200));

        return Task.FromResult(Result.SuccessResult());
    }
}
