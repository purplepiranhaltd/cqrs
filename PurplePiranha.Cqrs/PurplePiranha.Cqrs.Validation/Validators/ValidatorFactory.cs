namespace PurplePiranha.Cqrs.Validation.Validators;

public class ValidatorFactory : IValidatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IValidator<TQuery> CreateValidator<TQuery>() where TQuery : IValidationRequired
    {
        var handler = _serviceProvider.GetService(typeof(IValidator<TQuery>));

        if (handler is null)
            throw ValidatorNotImplementedException.Create<TQuery>();

        return (IValidator<TQuery>)handler;
    }
}