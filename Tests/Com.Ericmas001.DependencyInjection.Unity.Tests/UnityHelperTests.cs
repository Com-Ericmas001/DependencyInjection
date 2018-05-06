using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Unity.Tests
{
    public class UnityHelperTests
    {
        [Fact]
        public void TestSimpleRegister()
        {
            //Arrange
            var container = new UnityContainer();
            new DynamicRegistrant(r => r.Register<Dummy>()).RegisterTypes(container);
            
            //Act
            var res = container.Resolve<Dummy>();
            
            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestImplementationRegister()
        {
            //Arrange
            var container = new UnityContainer();
            new DynamicRegistrant(r => r.Register<IDummy, Dummy>()).RegisterTypes(container);
            
            //Act
            var res = container.Resolve<IDummy>();
            
            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestImplementationRegisterNamed()
        {
            //Arrange
            var container = new UnityContainer();
            const string DUMB_NAME = "DumbName";
            new DynamicRegistrant(r => r.Register<IDummy, Dummy>(DUMB_NAME)).RegisterTypes(container);

            //Act
            var res = container.Resolve<IDummy>(DUMB_NAME);

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestSimpleRegisterWithFactory()
        {
            //Arrange
            var container = new UnityContainer();
            const string DUMB_NAME = "DumbName";
            DummyWithName CreatFunc() => new DummyWithName(DUMB_NAME);
            new DynamicRegistrant(r => r.Register(CreatFunc)).RegisterTypes(container);

            //Act
            var res = container.Resolve<DummyWithName>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<DummyWithName>();
            res.Name.Should().Be(DUMB_NAME);
        }
        [Fact]
        public void TestImplementationRegisterWithFactory()
        {
            //Arrange
            var container = new UnityContainer();
            const string DUMB_NAME = "DumbName";
            DummyWithName CreatFunc() => new DummyWithName(DUMB_NAME);
            new DynamicRegistrant(r => r.Register<IDummy, DummyWithName>(CreatFunc)).RegisterTypes(container);

            //Act
            var res = container.Resolve<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<DummyWithName>();
            ((DummyWithName)res).Name.Should().Be(DUMB_NAME);
        }
        [Fact]
        public void TestImplementationRegisterNamedWithFactory()
        {
            //Arrange
            var container = new UnityContainer();
            const string NAME = "MyName";
            const string DUMB_NAME = "DumbName";
            DummyWithName CreatFunc() => new DummyWithName(DUMB_NAME);
            new DynamicRegistrant(r => r.Register<IDummy, DummyWithName>(NAME, CreatFunc)).RegisterTypes(container);

            //Act
            var res = container.Resolve<IDummy>(NAME);

            //Assert
            res.Should().NotBeNull().And.BeOfType<DummyWithName>();
            ((DummyWithName)res).Name.Should().Be(DUMB_NAME);
        }
        [Fact]
        public void TestRegisterInstance()
        {
            //Arrange
            var container = new UnityContainer();
            Dummy instance = new Dummy();
            new DynamicRegistrant(r => r.RegisterInstance(instance)).RegisterTypes(container);

            //Act
            var res = container.Resolve<Dummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestRegisterInstanceImplementingInterface()
        {
            //Arrange
            var container = new UnityContainer();
            Dummy instance = new Dummy();
            new DynamicRegistrant(r => r.RegisterInstance<IDummy>(instance)).RegisterTypes(container);

            //Act
            var res = container.Resolve<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
