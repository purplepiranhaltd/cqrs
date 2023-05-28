using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Commands.ServiceCollection
{
    public class ServiceCollectionMock
    {
        private readonly Mock<IServiceCollection> serviceCollectionMock;

        public ServiceCollectionMock()
        {
            serviceCollectionMock = new Mock<IServiceCollection>();
        }

        public IServiceCollection ServiceCollection => serviceCollectionMock.Object;
    }
}
