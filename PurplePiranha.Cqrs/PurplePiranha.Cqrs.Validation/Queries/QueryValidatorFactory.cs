namespace PurplePiranha.Cqrs.Validation.Queries;

public class QueryValidatorFactory : IQueryValidatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public QueryValidatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IQueryValidationHandler<TQuery> CreateValidationHandler<TQuery>() where TQuery : IValidatingQuery
    {
        var handler = _serviceProvider.GetService(typeof(IQueryValidationHandler<TQuery>));

        if (handler is null)
            throw QueryValidationHandlerNotImplementedException.Create<TQuery>();

        return (IQueryValidationHandler<TQuery>)handler;
    }
}