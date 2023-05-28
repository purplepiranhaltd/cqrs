using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework.Internal;
using PurplePiranha.Cqrs.Commands;
using PurplePiranha.Cqrs.Extensions;
using PurplePiranha.Cqrs.Tests.Commands.CommandWithoutReturnType;
using PurplePiranha.Cqrs.Tests.Commands.CommandWithSimpleReturnType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePiranha.Cqrs.Tests.Commands.ServiceCollection
{
    public class ServiceCollectionUnitTests
    {
        private Mock<IServiceCollection> _serviceCollectionMock;
        private ServiceCollectionVerifier _serviceCollectionVerifier;

        [SetUp]
        public void Setup()
        {
            _serviceCollectionMock = new Mock<IServiceCollection>();
            _serviceCollectionVerifier = new ServiceCollectionVerifier(_serviceCollectionMock);
            _serviceCollectionMock.Object.AddCqrsServices();
        }


        [Test]
        public async Task Test_CommandHandlerRegistered_WithoutReturnType()
        {
            _serviceCollectionVerifier.ContainsScopedService<ICommandHandler<ACommandWithoutReturnType>, ACommandWithoutReturnTypeHandler>();
        }

        [Test]
        public async Task Test_CommandHandlerRegistered_WithoutReturnType2()
        {
            _serviceCollectionVerifier.ContainsScopedService<ICommandHandler<ACommandWithSimpleReturnType, int>, ACommandWithSimpleReturnTypeHandler>();
        }
    }
}
