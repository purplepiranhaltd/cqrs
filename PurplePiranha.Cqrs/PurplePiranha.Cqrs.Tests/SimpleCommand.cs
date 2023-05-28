using PurplePiranha.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests
{
    /// <summary>
    /// A simple command that has two input values
    /// </summary>
    public record SimpleCommand(int A, int B) : ICommand
    {
    }
}
