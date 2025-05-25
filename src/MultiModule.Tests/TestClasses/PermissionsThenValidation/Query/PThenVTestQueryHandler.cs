using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.PermissionsThenValidation.Query;

public class PThenVTestQueryHandler : IQueryHandler<PThenVTestQuery, int>
{
    public Task<Result<int>> ExecuteAsync(PThenVTestQuery query, CancellationToken cancellationToken = default)
    {
        // Ensure that query isn't executed before permission checking
        Assert.That(query.SpecialNumber, Is.Not.EqualTo(100));

        // Ensure that query isn't executed before validation check
        Assert.That(query.SpecialNumber, Is.Not.EqualTo(200));

        return Task.FromResult(Result.SuccessResult(query.SpecialNumber));
    }
}
