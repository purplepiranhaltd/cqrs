using PurplePiranha.Cqrs.Permissions.Decorators;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.Validation.Validators;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.PermissionsThenValidation.Query
{
    /// <summary>
    /// Test Query
    /// Uses special numbers:
    /// 100 Not Authorised
    /// 200 Validation Failure
    /// Otherwise returns SpecialNumber
    /// </summary>
    /// <seealso cref="IQuery&lt;int&gt;" />
    /// <seealso cref="IEquatable&lt;ValidationAndPermissionsTestQuery&gt;" />
    public record PThenVTestQuery(int SpecialNumber) : IQuery<int>, IPermissionRequired, IValidationRequired
    {
    }
}
