using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.ValidationThenPermissions.Command;

public class VThenPTestCommandHandler : ICommandHandler<VThenPTestCommand>
{
    public Task<Result> ExecuteAsync(VThenPTestCommand command, CancellationToken cancellationToken = default)
    {
        // Ensure that query isn't executed before permission checking
        Assert.That(command.SpecialNumber, Is.Not.EqualTo(100));

        // Ensure that query isn't executed before validation check
        Assert.That(command.SpecialNumber, Is.Not.EqualTo(200));

        return Task.FromResult(Result.SuccessResult());
    }
}
