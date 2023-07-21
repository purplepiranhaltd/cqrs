using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.ValidationThenPermissions.CommandT;

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
public record VThenPTestCommandT(int SpecialNumber) : ICommand<int>, IValidationRequired, IPermissionRequired
{
}
