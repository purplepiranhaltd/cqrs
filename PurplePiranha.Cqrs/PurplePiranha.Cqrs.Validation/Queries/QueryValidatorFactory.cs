using PurplePiranha.Cqrs.Exceptions;

namespace PurplePiranha.Cqrs.Queries.Validation;

public class QueryValidatorFactory : IQueryValidatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public QueryValidatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IQueryValidator<TQuery> CreateValidator<TQuery>() where TQuery : IValidatingQuery
    {
        var handler = _serviceProvider.GetService(typeof(IQueryValidator<TQuery>));

        if (handler is null)
            throw QueryValidationHandlerNotImplementedException.Create<TQuery>();

        return (IQueryValidator<TQuery>)handler;
    }
}
