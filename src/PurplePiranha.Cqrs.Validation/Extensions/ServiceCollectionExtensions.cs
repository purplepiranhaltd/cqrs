using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.ServiceRegistration;
using PurplePiranha.Cqrs.Validation.Commands;
using PurplePiranha.Cqrs.Validation.Queries;
using PurplePiranha.Cqrs.Validation.Validators;

namespace PurplePiranha.Cqrs.Validation.Extensions;

public static class ServiceCollectionExtensions
{
    #region Extension Methods

    public static IServiceCollection AddCqrsWithValidation(this IServiceCollection services)
    {
        // remove the default query and command executors
        services.RemoveAll<IQueryExecutor>(); 
        services.RemoveAll<ICommandExecutor>();

        services
            .AddCqrs()
            .AddScoped<IValidatorFactory, ValidatorFactory>()
            .AddScoped<IValidatorExecutor, ValidatorExecutor>()
            .AddScoped<IValidatingQueryExecutor, ValidatingQueryExecutor>() 
            .AddScoped<IValidatingCommandExecutor, ValidatingCommandExecutor>()
            .AddCqrsValidationHandlers();



        return services;
    }

    #endregion Extension Methods

    #region Helpers

    private static IServiceCollection AddCqrsValidationHandlers(this IServiceCollection services)
    {
        var handlerRegistrar = new HandlerRegistrar(new Type[]
        {
        typeof(FluentValidation.IValidator<>)
        });

        handlerRegistrar.RegisterHandlers(services);

        return services;
    }

    #endregion Helpers
}