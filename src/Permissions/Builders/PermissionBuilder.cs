using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Exceptions;

namespace PurplePiranha.Cqrs.Permissions.Builders;

public class PermissionBuilder :
        IPermissionBuilder,
        IPermissionBuilderInitial,
        IPermissionBuilderWithCondition,
        IPermissionBuilderFinal

{
    protected List<(PermissionBuilderMode mode, Task<bool> condition, bool negate)> _permissionCalculation;
    protected PermissionBuilderMode _mode;
    protected PermissionBuilderOutcome _definedOutcome;

    protected PermissionBuilderOutcome? _finalOutcome;

    public PermissionBuilderOutcome Result
    {
        get
        {
            if (_finalOutcome is null)
                throw new PermissionRuleHasNotBeenBuiltException();

            return _finalOutcome.Value;
        }
    }

    public PermissionBuilder()
    {
        this._mode = PermissionBuilderMode.Or;
        this._permissionCalculation = new List<(PermissionBuilderMode mode, Task<bool> condition, bool negate)>();
        this._definedOutcome = PermissionBuilderOutcome.None;
    }

    public async Task Build()
    {
        if (this._definedOutcome == PermissionBuilderOutcome.None)
            throw new PermissionRuleHasNotBeenCompletedException();

        var matchesRule = false;

        foreach (var rule in _permissionCalculation)
        {
            var condition = await rule.condition;

            switch (rule.mode)
            {
                case PermissionBuilderMode.And:
                    matchesRule = matchesRule && condition;
                    break;
                case PermissionBuilderMode.Or:
                    matchesRule = matchesRule || condition;
                    break;
            }

            if (rule.negate)
                matchesRule = !matchesRule;
        }

        if (matchesRule)
            _finalOutcome = _definedOutcome;
        else
            _finalOutcome = PermissionBuilderOutcome.None;
    }

    public IPermissionBuilderWithCondition IsTrue(Func<bool> func)
    {
        _permissionCalculation.Add((this._mode, Task.FromResult(func()), false));
        return this;
    }

    public IPermissionBuilderWithCondition IsFalse(Func<bool> func)
    {
        _permissionCalculation.Add((this._mode, Task.FromResult(func()), true));
        return this;
    }

    public IPermissionBuilderWithCondition IsTrue(Func<Task<bool>> func)
    {
        _permissionCalculation.Add((this._mode, func(), false));
        return this;
    }

    public IPermissionBuilderWithCondition IsFalse(Func<Task<bool>> func)
    {
        _permissionCalculation.Add((this._mode, func(), true));
        return this;
    }

    public IPermissionBuilderInitial And()
    {
        _mode = PermissionBuilderMode.And;
        return this;
    }

    public IPermissionBuilderInitial Or()
    {
        _mode = PermissionBuilderMode.Or;
        return this;
    }

    public IPermissionBuilderFinal Deny()
    {
        _definedOutcome = PermissionBuilderOutcome.Deny;
        return this;
    }

    public IPermissionBuilderFinal Grant()
    {
        _definedOutcome = PermissionBuilderOutcome.Grant;
        return this;
    }
}