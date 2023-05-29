using PurplePiranha.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Queries
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery, TResult> CreateHandler<TQuery, TResult>() where TQuery : IQuery<TResult>;
    }
}
