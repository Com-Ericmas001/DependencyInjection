using Autofac;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Autofac.Tests
{
    public class AutofacHelperTests
    {
        [Fact]
        public void TestSimpleRegister()
        {
            //Arrange
            var container = new ContainerBuilder();
            new DynamicRegistrant(r => r.Register<Dummy>()).RegisterTypes(container);
            var scope = container.Build().BeginLifetimeScope();

            //Act
            var res = scope.Resolve<Dummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestImplementationRegister()
        {
            //Arrange
            var container = new ContainerBuilder();
            new DynamicRegistrant(r => r.Register<IDummy, Dummy>()).RegisterTypes(container);
            var scope = container.Build().BeginLifetimeScope();

            //Act
            var res = scope.Resolve<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestImplementationRegisterNamed()
        {
            //Arrange
            var container = new ContainerBuilder();
            const string DUMB_NAME = "DumbName";
            new DynamicRegistrant(r => r.Register<IDummy, Dummy>(DUMB_NAME)).RegisterTypes(container);
            var scope = container.Build().BeginLifetimeScope();

            //Act
            var res = scope.ResolveNamed<IDummy>(DUMB_NAME);

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestSimpleRegisterWithFactory()
        {
            //Arrange
            var container = new ContainerBuilder();
            const string DUMB_NAME = "DumbName";
            DummyWithName CreatFunc(IResolverService resolver) => new DummyWithName(DUMB_NAME);
            new DynamicRegistrant(r => r.Register(CreatFunc)).RegisterTypes(container);
            var scope = container.Build().BeginLifetimeScope();

            //Act
            var res = scope.Resolve<DummyWithName>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<DummyWithName>();
            res.Name.Should().Be(DUMB_NAME);
        }
        [Fact]
        public void TestImplementationRegisterWithFactory()
        {
            //Arrange
            var container = new ContainerBuilder();
            const string DUMB_NAME = "DumbName";
            DummyWithName CreatFunc(IResolverService resolver) => new DummyWithName(DUMB_NAME);
            new DynamicRegistrant(r => r.Register<IDummy, DummyWithName>(factory: CreatFunc)).RegisterTypes(container);
            var scope = container.Build().BeginLifetimeScope();

            //Act
            var res = scope.Resolve<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<DummyWithName>();
            ((DummyWithName)res).Name.Should().Be(DUMB_NAME);
        }
        [Fact]
        public void TestImplementationRegisterNamedWithFactory()
        {
            //Arrange
            var container = new ContainerBuilder();
            const string NAME = "MyName";
            const string DUMB_NAME = "DumbName";
            DummyWithName CreatFunc(IResolverService resolver) => new DummyWithName(DUMB_NAME);
            new DynamicRegistrant(r => r.Register<IDummy, DummyWithName>(NAME, CreatFunc)).RegisterTypes(container);
            var scope = container.Build().BeginLifetimeScope();

            //Act
            var res = scope.ResolveNamed<IDummy>(NAME);

            //Assert
            res.Should().NotBeNull().And.BeOfType<DummyWithName>();
            ((DummyWithName)res).Name.Should().Be(DUMB_NAME);
        }
        [Fact]
        public void TestRegisterInstance()
        {
            //Arrange
            var container = new ContainerBuilder();
            Dummy instance = new Dummy();
            new DynamicRegistrant(r => r.RegisterInstance(instance)).RegisterTypes(container);
            var scope = container.Build().BeginLifetimeScope();

            //Act
            var res = scope.Resolve<Dummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestRegisterInstanceImplementingInterface()
        {
            //Arrange
            var container = new ContainerBuilder();
            Dummy instance = new Dummy();
            new DynamicRegistrant(r => r.RegisterInstance<IDummy>(instance)).RegisterTypes(container);
            var scope = container.Build().BeginLifetimeScope();

            //Act
            var res = scope.Resolve<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
