using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Builders;
using System.Linq.Expressions;

namespace PurplePiranha.Cqrs.Permissions.PermissionCheckers;
public abstract class AbstractPermissionChecker<T> : IPermissionChecker<T>
{
    List<PermissionBuilder> _builders;
    protected T Object { get; private set; }

    public AbstractPermissionChecker()
    {
        _builders = new List<PermissionBuilder>();
    }

    public IPermissionBuilderWithPropertyInitial<T, TProperty> PermissionFor<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        var func = expression.Compile();
        var value = func(Object);

        var builder = new PermissionBuilder<T, TProperty>((TProperty)value);
        _builders.Add(builder);
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
        _builders.Add(builder);
        return builder;
    }

    public async Task<bool> HasPermission(T obj)
    {
        Object = obj;
        await Permissions();

        foreach (var builder in _builders)
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
