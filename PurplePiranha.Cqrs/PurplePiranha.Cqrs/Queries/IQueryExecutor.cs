using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Queries
{
    public interface IQueryExecutor
    {
        Task<Result<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}
