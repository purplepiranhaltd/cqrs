using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.ServiceRegistration;
using System;

namespace PurplePiranha.Cqrs.Extensions;

public static class ServiceCollectionExtensions
{
    #region Extension Methods

    /// <summary>
    /// Adds the CQRS services to the service collection.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        // Register default command and query executors by interface
        services.AddScoped<ICommandExecutor, CommandExecutor>();
        services.AddScoped<IQueryExecutor, QueryExecutor>();

        // Register default command and query executors by concrete type.
        // This allows them to still be accessible if we need to override them.
        services.AddScoped<CommandExecutor>();
        services.AddScoped<QueryExecutor>();

        // Register factories
        services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
        services.AddScoped<IQueryHandlerFactory, QueryHandlerFactory>();

        // Register all command and query handlers
        services.AddCqrsHandlers(typeof(ICommandHandler<>), typeof(ICommandHandler<,>), typeof(IQueryHandler<,>));
        return services;
    }

    #endregion Extension Methods

    #region Helpers

    /// <summary>
    /// Adds the CQRS handlers to the service collection.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddCqrsHandlers(this IServiceCollection services, params Type[] handlerTypes)
    {
        var handlerRegistrar = new HandlerRegistrar(handlerTypes);
        handlerRegistrar.RegisterHandlers(services);
        return services;
    }

    #endregion Helpers
}