using PurplePiranha.Cqrs.Core.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Core.Tests.TestClasses.Queries;

public class TestQueryHandler : IQueryHandler<TestQuery, string>
{
    private static string[] Data = { "Zero", "One", "Two" };

    public async Task<Result<string>> ExecuteAsync(TestQuery query, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Result.SuccessResult(Data[query.Id]));
    }
}