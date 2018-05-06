using Autofac;
using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Autofac.Tests
{
    public class AutofacResolverServiceTest
    {
        [Fact]
        public void TestGenericResolve()
        {
            //Arrange
            var container = new ContainerBuilder();
            container.RegisterType<Dummy>();
            var scope = container.Build().BeginLifetimeScope();
            var resolver = new AutofacResolverService(){Scope = scope};

            //Act
            var res = resolver.Resolve<Dummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestTypeResolve()
        {
            //Arrange
            var container = new ContainerBuilder();
            container.RegisterType<Dummy>();
            var scope = container.Build().BeginLifetimeScope();
            var resolver = new AutofacResolverService() { Scope = scope };

            //Act
            var res = resolver.Resolve(typeof(Dummy));

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
