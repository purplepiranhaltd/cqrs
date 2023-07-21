using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Executors;
using PurplePiranha.Cqrs.Permissions.Factories;
using PurplePiranha.Cqrs.Permissions.Options;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.Cqrs.ServiceRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.ServiceRegistration;

public static class ServiceCollectionExtensions
{
    #region Extension Methods
    public static IServiceCollection WithCqrsPermissionsModule(
        this IServiceCollection services, CqrsPermissionsOptions? options = null
        )
    {
        if (options is null)
            options = new CqrsPermissionsOptions();

        // Get the current command and query executor types.
        // We will need to pass these to the constructors of our new ones.
        var helper = new ServiceRegistrationHelper(services);
        var queryExecutorType = helper.GetCurrentImplementationType<IQueryExecutor>();
        var commandExecutorType = helper.GetCurrentImplementationType<ICommandExecutor>();

        // Remove the current command and query executor types registered by interface.
        // They will still be accessible as they are also registered via their concrete type.
        services.RemoveAll(typeof(IQueryExecutor));
        services.RemoveAll(typeof(ICommandExecutor));

        // Register permission checker factory and executor
        services
            .AddScoped<IPermissionCheckerFactory, PermissionCheckerFactory>()
            .AddScoped<IPermissionCheckerExecutor, PermissionCheckerExecutor>();

        // Register the permission checking command and query executors by interface.
        // We pass the previous command and query executors to them.
        services
            .AddScoped<IQueryExecutor, PermissionCheckingQueryExecutor>(x =>
            {
                return new PermissionCheckingQueryExecutor(
                    (IQueryExecutor)x.GetRequiredService(queryExecutorType),
                    x.GetRequiredService<IPermissionCheckerExecutor>(),
                    x.GetRequiredService<NotAuthorisedFailureFactory>()
                    );
            })
            .AddScoped<ICommandExecutor, PermissionCheckingCommandExecutor>(x =>
            {
                return new PermissionCheckingCommandExecutor(
                    (ICommandExecutor)x.GetRequiredService(commandExecutorType),
                    x.GetRequiredService<IPermissionCheckerExecutor>(),
                    x.GetRequiredService<NotAuthorisedFailureFactory>()
                    );
            });

        // Also register the permission checking command and query executors by their concrete type.
        // This allows them to still be accessible if we need to override them.
        services
            .AddScoped(x =>
            {
                return new PermissionCheckingQueryExecutor(
                    (IQueryExecutor)x.GetRequiredService(queryExecutorType),
                    x.GetRequiredService<IPermissionCheckerExecutor>(),
                    x.GetRequiredService<NotAuthorisedFailureFactory>()
                    );
            })
            .AddScoped(x =>
            {
                return new PermissionCheckingCommandExecutor(
                    (ICommandExecutor)x.GetRequiredService(commandExecutorType),
                    x.GetRequiredService<IPermissionCheckerExecutor>(),
                    x.GetRequiredService<NotAuthorisedFailureFactory>()
                    );
            });

        // Register all permission checkers
        services
            .AddCqrsHandlers(typeof(IPermissionChecker<>));

        // Register the NotAuthorisedFailureFactory.
        // This allows the use of a custom failure type which is useful in a system that
        // already has a failure defined for dealing with a Not Authorised scenario.
        services.AddScoped<NotAuthorisedFailureFactory>(x => {
            return new NotAuthorisedFailureFactory(options.NotAuthorisedFailureType);
        });


        return services;
    }

    #endregion
}
