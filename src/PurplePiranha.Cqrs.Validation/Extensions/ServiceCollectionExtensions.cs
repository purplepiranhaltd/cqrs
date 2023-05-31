using Microsoft.Extensions.DependencyInjection;
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
        services
            .AddCqrs()

            .AddScoped<IValidatorFactory, ValidatorFactory>()
            .AddScoped<IValidatorExecutor, ValidatorExecutor>()

            // override original QueryExecutor & CommandExecutor
            .AddScoped<IQueryExecutor, QueryExecutorWithValidation>() 
            .AddScoped<ICommandExecutor, CommandExecutorWithValidation>()

            .AddCqrsValidationHandlers();

        return services;
    }

    #endregion Extension Methods

    #region Helpers

    private static IServiceCollection AddCqrsValidationHandlers(this IServiceCollection services)
    {
        var handlerRegistrar = new HandlerRegistrar(new Type[]
        {
        typeof(IValidator<>)
        });

        handlerRegistrar.RegisterHandlers(services);

        return services;
    }

    #endregion Helpers
}