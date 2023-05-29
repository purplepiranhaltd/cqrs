using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Queries
{
    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        private static string[] Data = { "Zero", "One", "Two" };

        public async Task<Result<string>> ExecuteAsync(TestQuery query)
        {
            return await Task.FromResult(Result.SuccessResult(Data[query.Id]));
        }
    }
}
