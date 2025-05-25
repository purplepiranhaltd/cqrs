using FluentValidation;

namespace PurplePiranha.Cqrs.Validation.Validators;

public class ValidatorFactory : IValidatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IValidator<T> CreateValidator<T>() where T : IValidationRequired
    {
        var handler = _serviceProvider.GetService(typeof(IValidator<T>));

        if (handler is null)
            throw ValidatorNotImplementedException.Create<T>();

        return (IValidator<T>)handler;
    }
}