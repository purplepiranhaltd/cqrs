using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.ValidationThenPermissions.Query;

public class VThenPTestQueryHandler : IQueryHandler<VThenPTestQuery, int>
{
    public Task<Result<int>> ExecuteAsync(VThenPTestQuery query, CancellationToken cancellationToken = default)
    {
        // Ensure that query isn't executed before permission checking
        Assert.That(query.SpecialNumber, Is.Not.EqualTo(100));

        // Ensure that query isn't executed before validation check
        Assert.That(query.SpecialNumber, Is.Not.EqualTo(200));

        return Task.FromResult(Result.SuccessResult(query.SpecialNumber));
    }
}
