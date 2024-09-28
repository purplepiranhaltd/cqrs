using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Validation.Tests.TestClasses.Queries;

public class TestValidatingQueryHandler : IQueryHandler<TestValidatingQuery, int>
{
    public async Task<Result<int>> ExecuteAsync(TestValidatingQuery query, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.SuccessResult(query.A * query.A));
    }
}