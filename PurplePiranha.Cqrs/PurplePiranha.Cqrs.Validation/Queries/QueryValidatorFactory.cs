using PurplePiranha.Cqrs.Validation.Exceptions;

namespace PurplePiranha.Cqrs.Validation.Queries;

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