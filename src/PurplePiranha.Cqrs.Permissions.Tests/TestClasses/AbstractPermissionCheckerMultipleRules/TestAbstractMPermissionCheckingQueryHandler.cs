using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public class TestAbstractMPermissionCheckingQueryHandler : IQueryHandler<TestAbstractMPermissionCheckingQuery, int>
{
    public Task<Result<int>> ExecuteAsync(TestAbstractMPermissionCheckingQuery query)
    {
        return Task.FromResult(Result.SuccessResult(100));
    }
}