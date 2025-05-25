using PurplePiranha.FluentResults.FailureTypes;

namespace PurplePiranha.Cqrs.Permissions.Factories;

public class NotAuthorisedFailureFactory
{
    private Type _failureType;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotAuthorisedFailureFactory"/> class
    /// to use a particular failure type.
    /// </summary>
    /// <param name="failureType">Type of the failure.</param>
    public NotAuthorisedFailureFactory(Type failureType) 
    {
        _failureType = failureType;
    }

    public Failure GetNotAuthorisedFailure()
    {
        var failure = Activator.CreateInstance(_failureType);

        if (failure is null)
            throw new NullReferenceException(nameof(failure));

        return (Failure)failure;
    }
}
