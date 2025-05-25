using PurplePiranha.Cqrs.Permissions.Failures;

namespace PurplePiranha.Cqrs.Permissions.ServiceRegistration;

public class CqrsPermissionsOptions
{
    public Type NotAuthorisedFailureType { get; set; } = typeof(NotAuthorisedFailure);
}
