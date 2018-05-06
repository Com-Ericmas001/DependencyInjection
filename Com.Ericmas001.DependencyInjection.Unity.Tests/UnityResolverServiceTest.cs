using System;
using System.Linq;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Unity.Tests
{
    public class UnityResolverServiceTest
    {
        [Fact]
        public void TestGenericResolve()
        {
            //Arrange
            var container = new UnityContainer();
            container.RegisterType<Dummy>();
            var resolver = new UnityResolverService(container);

            //Act
            var res = resolver.Resolve<Dummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestTypeResolve()
        {
            //Arrange
            var container = new UnityContainer();
            container.RegisterType<Dummy>();
            var resolver = new UnityResolverService(container);

            //Act
            var res = resolver.Resolve(typeof(Dummy));

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
