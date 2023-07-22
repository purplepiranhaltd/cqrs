using PurplePiranha.Cqrs.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Permissions.Builders;

public class PermissionBuilder<T, TProperty> :
    PermissionBuilder,
    IPermissionBuilder,
    IPermissionBuilderWithPropertyInitial<T, TProperty>,
    IPermissionBuilderWithPropertyWithCondition<T, TProperty>
{
    TProperty _propertyValue;

    public PermissionBuilder(TProperty propertyValue)
    {
        _propertyValue = propertyValue;
    }

    public IPermissionBuilderWithPropertyInitial<T, TProperty> And()
    {
        _mode = PermissionBuilderMode.And;
        return this;
    }

    public IPermissionBuilderWithPropertyInitial<T, TProperty> Or()
    {
        _mode = PermissionBuilderMode.Or;
        return this;
    }

    public IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsEqualTo(TProperty value)
    {
        _permissionCalculation.Add((this._mode, Task.FromResult(_propertyValue.Equals(value)), false));
        return this;
    }

    public IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsNotEqualTo(TProperty value)
    {
        _permissionCalculation.Add((this._mode, Task.FromResult(_propertyValue.Equals(value)), true));
        return this;
    }

    public IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsTrue(Func<TProperty, bool> func)
    {
        _permissionCalculation.Add((this._mode, Task.FromResult(func(_propertyValue)), false));
        return this;
    }

    public IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsFalse(Func<TProperty, bool> func)
    {
        _permissionCalculation.Add((this._mode, Task.FromResult(func(_propertyValue)), true));
        return this;
    }

    public IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsTrue(Func<TProperty, Task<bool>> func)
    {
        _permissionCalculation.Add((_mode, func(_propertyValue), false));
        return this;
    }

    public IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsFalse(Func<TProperty, Task<bool>> func)
    {
        _permissionCalculation.Add((_mode, func(_propertyValue), true));
        return this;
    }

    public IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsIn(IEnumerable<TProperty> value)
    {
        _permissionCalculation.Add((this._mode, Task.FromResult(value.Contains(_propertyValue)), false));
        return this;
    }



    public IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsNotIn(IEnumerable<TProperty> value)
    {
        _permissionCalculation.Add((this._mode, Task.FromResult(value.Contains(_propertyValue)), true));
        return this;
    }
}
