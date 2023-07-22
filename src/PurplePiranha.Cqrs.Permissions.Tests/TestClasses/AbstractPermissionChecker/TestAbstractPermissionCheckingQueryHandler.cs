using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;

public class TestAbstractPermissionCheckingQueryHandler : IQueryHandler<TestAbstractPermissionCheckingQuery, int>
{
    public async Task<Result<int>> ExecuteAsync(TestAbstractPermissionCheckingQuery query)
    {
        if (query.IMustNotBe100 == 100)
            Assert.Fail("Query handler called before permission checking.");

        return await Task.FromResult(Result.SuccessResult(query.IMustNotBe100 * 2));
    }
}