using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.AbstractPermissionChecker;

public class TestAbstractPermissionCheckingQueryHandler : IQueryHandler<TestAbstractPermissionCheckingQuery, int>
{
    public async Task<Result<int>> ExecuteAsync(TestAbstractPermissionCheckingQuery query, CancellationToken cancellationToken = default)
    {
        if (query.IMustNotBe100 == 100)
            Assert.Fail("Query handler called before permission checking.");

        return await Task.FromResult(Result.SuccessResult(query.IMustNotBe100 * 2));
    }
}