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

    public static IServiceCollection WithCqrsValidation(this IServiceCollection services)
    {
        // Get the currently registered IQueryExecutor and then register it's concrete type instead
        var queryExecutorType = services.Where(x => x.ServiceType == typeof(IQueryExecutor)).Select(x => x.ImplementationType).FirstOrDefault();

        if (queryExecutorType is null)
            throw new ArgumentNullException(nameof(queryExecutorType));

        services.AddScoped(queryExecutorType);
        services.RemoveAll<IQueryExecutor>();

        // Get the currently registered ICommandExecutor and then register it's concrete type instead
        var commandExecutorType = services.Where(x => x.ServiceType == typeof(ICommandExecutor)).Select(x => x.ImplementationType).FirstOrDefault();

        if (commandExecutorType is null)
            throw new ArgumentNullException(nameof(commandExecutorType));

        services.AddScoped(commandExecutorType);
        services.RemoveAll<ICommandExecutor>();

        services
            .AddScoped<IValidatorFactory, ValidatorFactory>()
            .AddScoped<IValidatorExecutor, ValidatorExecutor>()
            .AddScoped<IQueryExecutor, ValidatingQueryExecutor>(x => {
                return new ValidatingQueryExecutor(
                    (IQueryExecutor)x.GetRequiredService(queryExecutorType),
                    x.GetRequiredService<IValidatorExecutor>()
                    );
            }) 
            .AddScoped<ICommandExecutor, ValidatingCommandExecutor>(x => {
                return new ValidatingCommandExecutor(
                    (ICommandExecutor)x.GetRequiredService(commandExecutorType),
                    x.GetRequiredService<IValidatorExecutor>()
                    );
            })
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