using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.PermissionsThenValidation.Query
{
    public class PThenVTestQueryHandler : IQueryHandler<PThenVTestQuery, int>
    {
        public Task<Result<int>> ExecuteAsync(PThenVTestQuery query)
        {
            // Ensure that query isn't executed before permission checking
            Assert.That(query.SpecialNumber, Is.Not.EqualTo(100));

            // Ensure that query isn't executed before validation check
            Assert.That(query.SpecialNumber, Is.Not.EqualTo(200));

            return Task.FromResult(Result.SuccessResult(query.SpecialNumber));
        }
    }
}
