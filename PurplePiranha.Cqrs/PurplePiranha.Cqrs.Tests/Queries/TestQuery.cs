using PurplePiranha.Cqrs.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Queries
{
    public record TestQuery(int Id) : IQuery<string>
    {
    }
}
