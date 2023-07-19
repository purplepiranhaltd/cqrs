using PurplePiranha.Cqrs.Permissions.Failures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Options
{
    public class CqrsPermissionsOptions
    {
        public Type NotAuthorisedFailureType { get; set; } = typeof(NotAuthorisedFailure);
    }
}
