using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Builders;
using System.Linq.Expressions;

namespace PurplePiranha.Cqrs.Permissions.PermissionCheckers;
public abstract class AbstractPermissionChecker<T> : IPermissionChecker<T>
{
    protected List<PermissionBuilder> Builders { get; private set; }
    protected T Object { get; private set; }

    public AbstractPermissionChecker()
    {
        Builders = new List<PermissionBuilder>();
    }

    public IPermissionBuilderWithPropertyInitial<T, TProperty> PermissionFor<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        var func = expression.Compile();
        var value = func(Object);

        var builder = new PermissionBuilder<T, TProperty>((TProperty)value);
        Builders.Add(builder);
        return builder;
    }

    public abstract Task Permissions();


    /// <summary>
    /// Override the InitialiseAsync method provide any custom initialisation
    /// that can't be done via the constructor due to the requirement of async calls.
    /// </summary>
    /// <returns></returns>
    public virtual Task InitialiseAsync()
    {
        return Task.CompletedTask;
    }

    public IPermissionBuilderInitial Permission()
    {
        var builder = new PermissionBuilder();
        Builders.Add(builder);
        return builder;
    }

    public async Task<bool> HasPermission(T obj)
    {
        Object = obj;
        await Permissions();

        foreach (var builder in Builders)
        {
            await builder.Build();

            switch (builder.Result)
            {
                case PermissionBuilderOutcome.Grant:
                    return true;
                case PermissionBuilderOutcome.Deny:
                    return false;
            }
        }

        return false;
    }
}
