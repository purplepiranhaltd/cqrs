using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Builders;
using PurplePiranha.Cqrs.Permissions.Exceptions;
using PurplePiranha.Cqrs.Permissions.Extensions;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;
using System.Linq.Expressions;

namespace PurplePiranha.Cqrs.Permissions.Tests;

public class PermissionBuilderTUnitTests
{
    [SetUp]
    public void Setup()
    {
    }

    public class Test1<T>
    {
        T _obj;

        public Test1(T obj) 
        { 
            _obj = obj;
        }

        public IPermissionBuilderWithPropertyInitial<T, TProperty> PermissionFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));
            
            var member = expression.GetMember();
            var value = _obj.GetType().GetProperty(member.Name).GetValue(_obj, null);

            var builder = new PermissionBuilder<PermissionBuilderTestClass, TProperty>((TProperty)value);
            return (IPermissionBuilderWithPropertyInitial<T, TProperty>)builder;
        }
    }
    

    [Test]
    public async Task PermissionForA_IsEqualTo_CorrectValue_Grant_OutcomeEqualsGrant()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsEqualTo(10).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task PermissionForA_IsEqualTo_IncorrectValue_Grant_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsEqualTo(11).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsEqualTo_CorrectValue_Deny_OutcomeEqualsDeny()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsEqualTo(10).Deny() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Deny));
    }

    [Test]
    public async Task PermissionForA_IsEqualTo_IncorrectValue_Deny_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsEqualTo(11).Deny() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsIn_CorrectValue_Grant_OutcomeEqualsGrant()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsIn(new int[] { 65, 42, 10 }).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task PermissionForA_IsIn_IncorrectValue_Grant_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsIn(new int[] { 65, 42, 75 }).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsIn_CorrectValue_Deny_OutcomeEqualsDeny()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsIn(new int[] { 65, 42, 10 }).Deny() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Deny));
    }

    [Test]
    public async Task PermissionForA_IsIn_IncorrectValue_Deny_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsIn(new int[] { 65, 42, 75 }).Deny() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsNotEqualTo_CorrectValue_Grant_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsNotEqualTo(10).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsNotEqualTo_IncorrectValue_Grant_OutcomeEqualsGrant()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsNotEqualTo(11).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task PermissionForA_IsNotEqualTo_CorrectValue_Deny_OutcomeEqualNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsNotEqualTo(10).Deny() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsNotEqualTo_IncorrectValue_Deny_OutcomeEqualsDeny()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsNotEqualTo(11).Deny() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Deny));
    }





    [Test]
    public async Task PermissionForA_IsNotIn_CorrectValue_Grant_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsNotIn(new int[] { 65, 42, 10 }).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsNotIn_IncorrectValue_Grant_OutcomeEqualsGrant()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsNotIn(new int[] { 65, 42, 75 }).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task PermissionForA_IsNotIn_CorrectValue_Deny_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsNotIn(new int[] { 65, 42, 10 }).Deny() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsNotIn_IncorrectValue_Deny_OutcomeEqualsDeny()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsNotIn(new int[] { 65, 42, 75 }).Deny() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Deny));
    }

    [Test]
    public async Task PermissionForA_IsTrue_EqualsTrue_Grant_OutcomeEqualsGrant()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsTrue(x => { return x == 10; }).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }

    [Test]
    public async Task PermissionForA_IsTrue_EqualsFalse_Grant_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsTrue(x => { return x == 20; }).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsFalse_EqualsTrue_Grant_OutcomeEqualsNone()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsFalse(x => { return x == 10; }).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.None));
    }

    [Test]
    public async Task PermissionForA_IsFalse_EqualsFalse_Grant_OutcomeEqualsGrant()
    {
        var test = new Test1<PermissionBuilderTestClass>(new PermissionBuilderTestClass() { A = 10, B = 20 });
        var builder = test.PermissionFor(x => x.A).IsFalse(x => { return x == 20; }).Grant() as PermissionBuilder;
        await builder.Build();
        Assert.That(builder.Result, Is.EqualTo(PermissionBuilderOutcome.Grant));
    }
}