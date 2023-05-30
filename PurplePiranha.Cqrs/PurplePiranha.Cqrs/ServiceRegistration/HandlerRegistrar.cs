using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PurplePiranha.Cqrs.ServiceRegistration
{
    public class HandlerRegistrar
    {
        private readonly Type[] _handlerTypes;

        public HandlerRegistrar(Type[] handlerTypes) {
            _handlerTypes = handlerTypes;
        }

        public void RegisterHandlers(IServiceCollection services)
        {
            var implementationTypes = GetCommandHandlerTypes();
            foreach (var implementationType in implementationTypes)
            {
                var serviceType = GetCommandHandlerInterfaceType(implementationType);

                if (serviceType != null)
                    services.AddScoped(serviceType, implementationType);
            }
        }

        private IEnumerable<Type> GetCommandHandlerTypes()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x =>
                {
                    if (x.IsInterface || x.IsAbstract) return false;
                    foreach (var t in _handlerTypes)
                    {
                        if (t.IsAssignableFrom(x)) return true;
                    }

                    if (x.GetInterfaces().Any(i =>
                        i.IsGenericType && _handlerTypes.Contains(i.GetGenericTypeDefinition())
                        )
                    ) return true;
                    return false;
                });
            return types;
        }

        private Type? GetCommandHandlerInterfaceType(Type commandHandlerType)
        {
            return commandHandlerType.FindInterfaces(filter, _handlerTypes).FirstOrDefault();
        }

        private static TypeFilter filter = new TypeFilter((typeObj, criteriaObj) =>
        {
            if (criteriaObj is null)
                return false;

            var types = (Type[])criteriaObj;
            return (types.Select(t => t.Name).Contains(typeObj.Name));
        });
        
    }
}
