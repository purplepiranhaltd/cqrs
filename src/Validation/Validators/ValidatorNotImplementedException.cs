namespace PurplePiranha.Cqrs.Validation.Validators;

public class ValidatorNotImplementedException : Exception
{
    public ValidatorNotImplementedException()
    {
    }

    public ValidatorNotImplementedException(string message)
        : base(message)
    {
    }

    public ValidatorNotImplementedException(string message, Exception inner)
    : base(message, inner)
    {
    }

    public ValidatorNotImplementedException(Type type) : base($"Validator for '{type.Name}' has not been implemented.")
    {
    }

    public static ValidatorNotImplementedException Create<T>()
    {
        return new ValidatorNotImplementedException(typeof(T));
    }
}