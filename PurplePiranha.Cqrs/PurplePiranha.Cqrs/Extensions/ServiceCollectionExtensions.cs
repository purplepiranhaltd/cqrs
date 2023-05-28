using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
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

        var typeNames = (string[])criteriaObj;

        if (typeNames.Contains(typeObj.Name))
            return true;
        else
            return false;
    });

    private static Type commandHandlerWithoutReturnTypeInterfaceGenericType = typeof(ICommandHandler<>);
    private static Type commandHandlerWithReturnTypeInterfaceGenericType = typeof(ICommandHandler<,>);
    #endregion

    #region Extension Methods
    public static IServiceCollection AddCqrsServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
        services.AddScoped<ICommandExecutor, CommandExecutor>();

        var implementationTypes = GetCommandHandlerTypes();
        foreach (var implementationType in implementationTypes)
        {
            var serviceType = GetCommandHandlerInterfaceType(implementationType);

            if (serviceType != null)
                services.AddScoped(serviceType, implementationType);
        }

        return services;
    }
    #endregion

    #region Helpers
    private static IEnumerable<Type> GetCommandHandlerTypes()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x =>
            {
                if (x.IsInterface || x.IsAbstract) return false;
                if (commandHandlerWithoutReturnTypeInterfaceGenericType.IsAssignableFrom(x)) return true;
                if (commandHandlerWithReturnTypeInterfaceGenericType.IsAssignableFrom(x)) return true;
                if (x.GetInterfaces().Any(i => 
                    i.IsGenericType && (
                        i.GetGenericTypeDefinition() == commandHandlerWithoutReturnTypeInterfaceGenericType ||
                        i.GetGenericTypeDefinition() == commandHandlerWithReturnTypeInterfaceGenericType
                        )
                    )
                ) return true;
                return false;
            });
        return types;
    }

    private static Type? GetCommandHandlerInterfaceType(Type commandHandlerType)
    {
        return commandHandlerType.FindInterfaces(
            myFilter,
            new string[] { 
                commandHandlerWithoutReturnTypeInterfaceGenericType.Name,
                commandHandlerWithReturnTypeInterfaceGenericType.Name
            }
            ).FirstOrDefault();
    }
    #endregion
}
