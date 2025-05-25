namespace PurplePiranha.Cqrs.Permissions.Abstractions;

public interface IPermissionBuilder
{
}

public interface IPermissionBuilderInitial : IPermissionBuilder
{
    IPermissionBuilderWithCondition IsTrue(Func<bool> func);
    IPermissionBuilderWithCondition IsFalse(Func<bool> func);
    IPermissionBuilderWithCondition IsTrue(Func<Task<bool>> func);
    IPermissionBuilderWithCondition IsFalse(Func<Task<bool>> func);
}

public interface IPermissionBuilderWithPropertyInitial<T, TProperty> : IPermissionBuilder
{
    IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsEqualTo(TProperty value);
    IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsNotEqualTo(TProperty value);
    IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsIn(IEnumerable<TProperty> value);
    IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsNotIn(IEnumerable<TProperty> value);
    IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsTrue(Func<TProperty, bool> func);
    IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsFalse(Func<TProperty, bool> func);
    IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsTrue(Func<TProperty, Task<bool>> func);
    IPermissionBuilderWithPropertyWithCondition<T, TProperty> IsFalse(Func<TProperty, Task<bool>> func);
}

public interface IPermissionBuilderWithCondition : IPermissionBuilder
{
    IPermissionBuilderFinal Grant();
    IPermissionBuilderFinal Deny();

    IPermissionBuilderInitial And();
    IPermissionBuilderInitial Or();
}

public interface IPermissionBuilderWithPropertyWithCondition<T, TProperty> : IPermissionBuilder
{
    IPermissionBuilderFinal Grant();
    IPermissionBuilderFinal Deny();

    IPermissionBuilderWithPropertyInitial<T, TProperty> And();
    IPermissionBuilderWithPropertyInitial<T, TProperty> Or();
}

public interface IPermissionBuilderFinal : IPermissionBuilder
{

}
