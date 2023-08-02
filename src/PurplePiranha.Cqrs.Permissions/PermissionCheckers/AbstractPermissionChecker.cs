using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Builders;
using PurplePiranha.Cqrs.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.PermissionCheckers;
public abstract class AbstractPermissionChecker<T> : IPermissionChecker<T>
{
    List<PermissionBuilder> _builders;
    T _obj;

    public AbstractPermissionChecker()
    {
        _builders = new List<PermissionBuilder>();
    }

    public IPermissionBuilderWithPropertyInitial<T, TProperty> PermissionFor<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        var member = expression.GetMember();
        var value = _obj.GetType().GetProperty(member.Name).GetValue(_obj, null);

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
        _obj = obj;
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
