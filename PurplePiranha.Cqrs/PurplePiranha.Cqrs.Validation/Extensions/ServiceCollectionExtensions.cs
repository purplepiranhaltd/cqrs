using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.ServiceRegistration;
using PurplePiranha.Cqrs.Validation.Queries;

namespace PurplePiranha.Cqrs.Validation.Extensions;

public static class ServiceCollectionExtensions
{
    #region Extension Methods

    public static IServiceCollection AddCqrsWithValidation(this IServiceCollection services)
    {
        services
            .AddCqrs()
            .AddScoped<IQueryValidatorFactory, QueryValidatorFactory>()
            .AddScoped<IQueryValidationExecutor, QueryValidatorExecutor>()
            .AddScoped<IQueryExecutor, QueryExecutorWithValidation>() // override original
            .AddCqrsValidationHandlers();

        return services;
    }

    #endregion Extension Methods

    #region Helpers

    private static IServiceCollection AddCqrsValidationHandlers(this IServiceCollection services)
    {
        var handlerRegistrar = new HandlerRegistrar(new Type[]
        {
        typeof(IQueryValidationHandler<>)
        });

        handlerRegistrar.RegisterHandlers(services);

        return services;
    }

    #endregion Helpers
}