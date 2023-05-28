using PurplePiranha.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Commands.CommandWithSimpleReturnType
{
    public record ACommandWithSimpleReturnType(int A, int B) : ICommand<int>
    {
    }
}
