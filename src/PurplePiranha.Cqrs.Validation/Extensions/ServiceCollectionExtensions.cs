using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
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

    public static IServiceCollection WithCqrsValidationModule(this IServiceCollection services)
    {
        // Get the current command and query executor types.
        // We will need to pass these to the constructors of our new ones.
        var helper = new ServiceRegistrationHelper(services);
        var queryExecutorType = helper.GetCurrentImplementationType<IQueryExecutor>();
        var commandExecutorType = helper.GetCurrentImplementationType<ICommandExecutor>();

        // Remove the current command and query executor types registered by interface.
        // They will still be accessible as they are also registered via their concrete type.
        services.RemoveAll(typeof(IQueryExecutor));
        services.RemoveAll(typeof(ICommandExecutor));

        // Register validator factory and executor
        services
            .AddScoped<IValidatorFactory, ValidatorFactory>()
            .AddScoped<IValidatorExecutor, ValidatorExecutor>();

        // Register the validating command and query executors by interface.
        // We pass the previous command and query executors to them.
        services
            .AddScoped<IQueryExecutor, ValidatingQueryExecutor>(x => {
                return new ValidatingQueryExecutor(
                    (IQueryExecutor)x.GetRequiredService(queryExecutorType),
                    x.GetRequiredService<IValidatorExecutor>()
                    );
            })
            .AddScoped<ICommandExecutor, ValidatingCommandExecutor>(x => {
                return new ValidatingCommandExecutor(
                    (ICommandExecutor)x.GetRequiredService(commandExecutorType),
                    x.GetRequiredService<IValidatorExecutor>(),
                    x.GetRequiredService<ILogger<ValidatingCommandExecutor>>()
                    );
            });

        // Also register the validator command and query executors by their concrete type.
        // This allows them to still be accessible if we need to override them.
        services
            .AddScoped<ValidatingQueryExecutor>(x => {
                return new ValidatingQueryExecutor(
                    (IQueryExecutor)x.GetRequiredService(queryExecutorType),
                    x.GetRequiredService<IValidatorExecutor>()
                    );
            })
            .AddScoped<ValidatingCommandExecutor>(x => {
                return new ValidatingCommandExecutor(
                    (ICommandExecutor)x.GetRequiredService(commandExecutorType),
                    x.GetRequiredService<IValidatorExecutor>(),
                    x.GetRequiredService<ILogger<ValidatingCommandExecutor>>()
                    );
            });

        // Register all validators
        services.AddCqrsHandlers(typeof(FluentValidation.IValidator<>));

        return services;
    }

    #endregion Extension Methods
}