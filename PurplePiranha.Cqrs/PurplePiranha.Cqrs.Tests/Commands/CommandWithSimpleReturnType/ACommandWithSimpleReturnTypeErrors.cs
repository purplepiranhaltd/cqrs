using PurplePiranha.Cqrs.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Commands.CommandWithSimpleReturnType
{
    public static class ACommandWithSimpleReturnTypeErrors
    {
        public static readonly Error ArithmeticOverFlow = new Error(
            "ArithmeticOverFlow",
            "Arithmetic overflow error"
            );
    }
}
