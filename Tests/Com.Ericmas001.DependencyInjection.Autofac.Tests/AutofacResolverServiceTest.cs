using Autofac;
using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Autofac.Tests
{
    public class AutofacResolverServiceTest
    {
        private const string NAME = "SpongeBob";
        [Fact]
        public void TestGenericResolve()
        {
            //Arrange
            var container = new ContainerBuilder();
            container.RegisterType<Dummy>().As<IDummy>();
            var scope = container.Build().BeginLifetimeScope();
            var resolver = new AutofacResolverService() { Scope = scope };

            //Act
            var res = resolver.Resolve<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestTypeResolve()
        {
            //Arrange
            var container = new ContainerBuilder();
            container.RegisterType<Dummy>().As<IDummy>();
            var scope = container.Build().BeginLifetimeScope();
            var resolver = new AutofacResolverService() { Scope = scope };

            //Act
            var res = resolver.Resolve(typeof(IDummy));

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestGenericResolveName()
        {
            //Arrange
            var container = new ContainerBuilder();
            container.RegisterType<Dummy>().Named<IDummy>(NAME);
            var scope = container.Build().BeginLifetimeScope();
            var resolver = new AutofacResolverService() { Scope = scope };

            //Act
            var res = resolver.Resolve<IDummy>(NAME);

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestTypeResolveName()
        {
            //Arrange
            var container = new ContainerBuilder();
            container.RegisterType<Dummy>().Named<IDummy>(NAME);
            var scope = container.Build().BeginLifetimeScope();
            var resolver = new AutofacResolverService() { Scope = scope };

            //Act
            var res = resolver.Resolve(typeof(IDummy), NAME);

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
