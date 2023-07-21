using PurplePiranha.Cqrs.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Extra.Tests.TestClasses.PermissionsThenValidation.Query;

public class PThenVTestQueryPermissionChecker : IPermissionChecker<PThenVTestQuery>
{
    public Task<bool> HasPermission(PThenVTestQuery obj)
    {
        if (obj.SpecialNumber == 100)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }
}
