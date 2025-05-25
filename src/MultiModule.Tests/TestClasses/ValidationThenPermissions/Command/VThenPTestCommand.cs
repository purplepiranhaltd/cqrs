using PurplePiranha.Cqrs.Core.Commands;
using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Validation.Validators;

namespace PurplePiranha.Cqrs.MultiModule.Tests.TestClasses.ValidationThenPermissions.Command;

/// <summary>
/// /// Test Query
/// Uses special numbers:
/// 100 Not Authorised
/// 200 Validation Failure
/// </summary>
/// <seealso cref="ICommand" />
/// <seealso cref="IValidationRequired" />
/// <seealso cref="IPermissionRequired" />
/// <seealso cref="IEquatable&lt;ValidationAndPermissionsTestCommand&gt;" />
public record VThenPTestCommand(int SpecialNumber) : ICommand, IValidationRequired, IPermissionRequired
{
}
