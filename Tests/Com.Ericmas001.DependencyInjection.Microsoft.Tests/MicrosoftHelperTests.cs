using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Microsoft.Tests
{
    public class MicrosoftHelperTests
    {
        [Fact]
        public void TestSimpleRegister()
        {
            //Arrange
            IServiceCollection container = new ServiceCollection();
            new DynamicRegistrant(r => r.Register<Dummy>()).RegisterTypes(container);
            var provider = container.BuildServiceProvider();
            
            //Act
            var res = provider.GetService<Dummy>();
            
            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestImplementationRegister()
        {
            //Arrange
            IServiceCollection container = new ServiceCollection();
            new DynamicRegistrant(r => r.Register<IDummy, Dummy>()).RegisterTypes(container);
            var provider = container.BuildServiceProvider();

            //Act
            var res = provider.GetService<IDummy>();
            
            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestSimpleRegisterWithFactory()
        {
            //Arrange
            IServiceCollection container = new ServiceCollection();
            const string DUMB_NAME = "DumbName";
            DummyWithName CreatFunc() => new DummyWithName(DUMB_NAME);
            new DynamicRegistrant(r => r.Register(CreatFunc)).RegisterTypes(container);
            var provider = container.BuildServiceProvider();

            //Act
            var res = provider.GetService<DummyWithName>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<DummyWithName>();
            res.Name.Should().Be(DUMB_NAME);
        }
        [Fact]
        public void TestImplementationRegisterWithFactory()
        {
            //Arrange
            IServiceCollection container = new ServiceCollection();
            const string DUMB_NAME = "DumbName";
            DummyWithName CreatFunc() => new DummyWithName(DUMB_NAME);
            new DynamicRegistrant(r => r.Register<IDummy, DummyWithName>(CreatFunc)).RegisterTypes(container);
            var provider = container.BuildServiceProvider();

            //Act
            var res = provider.GetService<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<DummyWithName>();
            ((DummyWithName)res).Name.Should().Be(DUMB_NAME);
        }
        [Fact]
        public void TestRegisterInstance()
        {
            //Arrange
            IServiceCollection container = new ServiceCollection();
            Dummy instance = new Dummy();
            new DynamicRegistrant(r => r.RegisterInstance(instance)).RegisterTypes(container);
            var provider = container.BuildServiceProvider();

            //Act
            var res = provider.GetService<Dummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestRegisterInstanceImplementingInterface()
        {
            //Arrange
            IServiceCollection container = new ServiceCollection();
            Dummy instance = new Dummy();
            new DynamicRegistrant(r => r.RegisterInstance<IDummy>(instance)).RegisterTypes(container);
            var provider = container.BuildServiceProvider();

            //Act
            var res = provider.GetService<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
