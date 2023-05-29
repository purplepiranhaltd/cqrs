using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PurplePiranha.Cqrs.Extensions;

public static class ServiceCollectionExtensions
{
    #region Fields
    private static TypeFilter myFilter = new TypeFilter((typeObj, criteriaObj) =>
    {
        if (criteriaObj is null)
            return false;

        var types = (Type[])criteriaObj;
        return (types.Select(t => t.Name).Contains(typeObj.Name));
    });

    // old way
    private static Type commandHandlerWithoutReturnTypeInterfaceGenericType = typeof(ICommandHandler<>);
    private static Type commandHandlerWithReturnTypeInterfaceGenericType = typeof(ICommandHandler<,>);

    // new way
    private static Type[] handlerGenericTypes = { typeof(ICommandHandler<>), typeof(ICommandHandler<,>), typeof(IQueryHandler<,>) };
    #endregion

    #region Extension Methods
    public static IServiceCollection AddCqrsServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
        services.AddScoped<ICommandExecutor, CommandExecutor>();
        services.AddScoped<IQueryHandlerFactory, QueryHandlerFactory>();
        services.AddScoped<IQueryExecutor, QueryExecutor>();
        services.AddCqrsHandlers();
        return services;
    }
    #endregion

    #region Helpers
    private static IServiceCollection AddCqrsHandlers(this IServiceCollection services)
    {
        var implementationTypes = GetCommandHandlerTypes();
        foreach (var implementationType in implementationTypes)
        {
            var serviceType = GetCommandHandlerInterfaceType(implementationType);

            if (serviceType != null)
                services.AddScoped(serviceType, implementationType);
        }

        return services;
    }
    private static IEnumerable<Type> GetCommandHandlerTypes()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x =>
            {
                if (x.IsInterface || x.IsAbstract) return false;
                foreach (var t in handlerGenericTypes)
                {
                    if (t.IsAssignableFrom(x)) return true;
                }
                
                if (x.GetInterfaces().Any(i => 
                    i.IsGenericType && handlerGenericTypes.Contains(i.GetGenericTypeDefinition())
                    )
                ) return true;
                return false;
            });
        return types;
    }

    private static Type? GetCommandHandlerInterfaceType(Type commandHandlerType)
    {
        return commandHandlerType.FindInterfaces(myFilter, handlerGenericTypes).FirstOrDefault();
    }
    #endregion
}
