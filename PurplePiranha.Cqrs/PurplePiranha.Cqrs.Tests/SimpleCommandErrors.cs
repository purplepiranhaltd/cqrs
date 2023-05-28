using PurplePiranha.Cqrs.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests
{
    public static class SimpleCommandErrors
    {
        public static readonly Error BothInputsMustBeEqual = new Error(
            "Equality",
            "Both inputs must be equal"
            );
    }
}
