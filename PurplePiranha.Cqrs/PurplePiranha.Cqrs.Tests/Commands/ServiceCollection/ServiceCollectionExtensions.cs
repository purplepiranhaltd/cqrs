using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Commands.ServiceCollection
{
    public static class ServiceDescriptionExtensions
    {
        public static bool Is<TService, TInstance>(this ServiceDescriptor serviceDescriptor, ServiceLifetime lifetime)
        {
            return serviceDescriptor.ServiceType == typeof(TService) &&
                   serviceDescriptor.ImplementationType == typeof(TInstance) &&
                   serviceDescriptor.Lifetime == lifetime;
        }
    }
}
