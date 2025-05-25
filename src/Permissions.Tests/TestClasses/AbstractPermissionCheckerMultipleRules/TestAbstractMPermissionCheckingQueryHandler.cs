using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.AbstractPermissionCheckerMultipleRules;

public class TestAbstractMPermissionCheckingQueryHandler : IQueryHandler<TestAbstractMPermissionCheckingQuery, int>
{
    public Task<Result<int>> ExecuteAsync(TestAbstractMPermissionCheckingQuery query, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.SuccessResult(100));
    }
}