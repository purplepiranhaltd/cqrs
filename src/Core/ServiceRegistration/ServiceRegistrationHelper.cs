using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace PurplePiranha.Cqrs.Core.ServiceRegistration;

public class ServiceRegistrationHelper
{
    private readonly IServiceCollection _services;

    public ServiceRegistrationHelper(IServiceCollection services)
    {
        _services = services;
    }

    public Type GetCurrentImplementationType<TServiceType>()
    {
        var descriptor = _services.Where(x => x.ServiceType == typeof(TServiceType)).FirstOrDefault();

        if (descriptor is null)
            throw new NullReferenceException(nameof(descriptor));

        return GetImplementationTypeFromServiceDescriptor(descriptor);
    }

    #region Helpers
    private Type GetImplementationTypeFromServiceDescriptor(ServiceDescriptor serviceDescriptor)
    {
        if (serviceDescriptor is null)
            throw new ArgumentNullException(nameof(serviceDescriptor));

        var type = serviceDescriptor.ImplementationType;

        if (type is null)
            type = GetTypeFromImplementationFactory(serviceDescriptor.ImplementationFactory);

        return type;
    }

    private static Type GetTypeFromImplementationFactory(Func<IServiceProvider, object>? implementationFactory)
    {
        if (implementationFactory is null)
            throw new ArgumentNullException(nameof(implementationFactory));

        var type = implementationFactory.Method.ReturnType;

        if (type is null)
            throw new NullReferenceException(nameof(type));

        return type;
    }
    #endregion Helpers
}
