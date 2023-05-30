using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.ServiceRegistration;
using System;

namespace PurplePiranha.Cqrs.Extensions;

public static class ServiceCollectionExtensions
{
    #region Fields
    

    
    #endregion

    #region Extension Methods
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
        services.AddScoped<ICommandExecutor, CommandExecutor>();
        services.AddScoped<IQueryHandlerFactory, QueryHandlerFactory>();
        //services.AddScoped<IQueryValidatorFactory, QueryValidatorFactory>();
        services.AddScoped<IQueryExecutor, QueryExecutor>();
        //services.AddScoped<IQueryValidatorExecutor, QueryValidatorExecutor>();
        services.AddCqrsHandlers();
        return services;
    }
    #endregion

    #region Helpers
    private static IServiceCollection AddCqrsHandlers(this IServiceCollection services)
    {
        var handlerRegistrar = new HandlerRegistrar(new Type[] 
        { 
            typeof(ICommandHandler<>), 
            typeof(ICommandHandler<,>), 
            typeof(IQueryHandler<,>) 
        });

        handlerRegistrar.RegisterHandlers(services);

        return services;
    }
    

    
    #endregion
}
