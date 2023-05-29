using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Queries
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IQueryHandler<TQuery, TResult> CreateHandler<TQuery, TResult>() where TQuery : IQuery<TResult>
        {
            var handler = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));

            if (handler is null)
                throw new HandlerNotImplementedException($"Query handler '{nameof(TQuery)}' with result '{nameof(TResult)}' has not been implemented.");

            return (IQueryHandler<TQuery, TResult>)handler;
        }
    }
}
