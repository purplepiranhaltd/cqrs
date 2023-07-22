using Microsoft.Extensions.DependencyInjection;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Permissions.Abstractions;
using PurplePiranha.Cqrs.Permissions.Builders;
using PurplePiranha.Cqrs.Permissions.Exceptions;
using PurplePiranha.Cqrs.Permissions.Failures;
using PurplePiranha.Cqrs.Permissions.ServiceRegistration;
using PurplePiranha.Cqrs.Permissions.Tests.TestClasses.Queries;
using PurplePiranha.Cqrs.Queries;
using PurplePiranha.FluentResults.Results;

namespace PurplePiranha.Cqrs.Permissions.Tests;

public class AbstractPermissionCheckerUnitTests
{
    private IQueryExecutor _queryExecutor;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs().WithCqrsPermissionsModule();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _queryExecutor = serviceProvider.GetRequiredService<IQueryExecutor>();
    }

    [Test]
    public async Task SingleRule_ShouldGrant()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestAbstractPermissionCheckingQuery(234));
        result.OnSuccess(r => { Assert.Pass(); });
        Assert.Fail();
    }

    [Test]
    public async Task SingleRule_ShouldDeny()
    {
        var result = await _queryExecutor.ExecuteAsync(new TestAbstractPermissionCheckingQuery(100));
        result.OnFailure(f => {
            if (f is NotAuthorisedFailure)
                Assert.Pass();
        });
        Assert.Fail();
    }

    [Test]
    public async Task MultipleRules_1_ShouldDeny() // isGuest Deny, userId = 1 Deny, userId = 2 Grant
    {
        var result = await _queryExecutor.ExecuteAsync(new TestAbstractMPermissionCheckingQuery(true, 2));
        result.OnFailure(f => {
            if (f is NotAuthorisedFailure)
                Assert.Pass();
        });
        Assert.Fail();
    }

    [Test]
    public async Task MultipleRules_2_ShouldDeny() // isGuest Deny, userId = 1 Deny, userId = 2 Grant
    {
        var result = await _queryExecutor.ExecuteAsync(new TestAbstractMPermissionCheckingQuery(false, 1));
        result.OnFailure(f => {
            if (f is NotAuthorisedFailure)
                Assert.Pass();
        });
        Assert.Fail();
    }

    [Test]
    public async Task MultipleRules_3_ShouldGrant() // isGuest Deny, userId = 1 Deny, userId = 2 Grant
    {
        var result = await _queryExecutor.ExecuteAsync(new TestAbstractMPermissionCheckingQuery(false, 2));
        result
            .OnSuccess(r => { 
                Assert.Pass(); 
            })
            .OnFailure(f => {
                if (f is NotAuthorisedFailure)
                    Assert.Fail();

            });
        Assert.Inconclusive();
    }

    [Test]
    public async Task MultipleRules_4_ShouldDeny() // isGuest Deny, userId = 1 Deny, userId = 2 Grant
    {
        var result = await _queryExecutor.ExecuteAsync(new TestAbstractMPermissionCheckingQuery(true, 2));
        result.OnFailure(f => {
            if (f is NotAuthorisedFailure)
                Assert.Pass();
        });
        Assert.Fail();
    }
}