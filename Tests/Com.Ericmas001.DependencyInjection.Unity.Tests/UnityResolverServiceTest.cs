using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Unity.Tests
{
    public class UnityResolverServiceTest
    {
        private const string NAME = "SpongeBob";
        [Fact]
        public void TestGenericResolve()
        {
            //Arrange
            var container = new UnityContainer();
            container.RegisterType<IDummy, Dummy>();
            var resolver = new UnityResolverService(container);

            //Act
            var res = resolver.Resolve<IDummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestTypeResolve()
        {
            //Arrange
            var container = new UnityContainer();
            container.RegisterType<IDummy, Dummy>();
            var resolver = new UnityResolverService(container);

            //Act
            var res = resolver.Resolve(typeof(IDummy));

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestGenericResolveName()
        {
            //Arrange
            var container = new UnityContainer();
            container.RegisterType<IDummy, Dummy>(NAME);
            var resolver = new UnityResolverService(container);

            //Act
            var res = resolver.Resolve<IDummy>(NAME);

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestTypeResolveName()
        {
            //Arrange
            var container = new UnityContainer();
            container.RegisterType<IDummy, Dummy>(NAME);
            var resolver = new UnityResolverService(container);

            //Act
            var res = resolver.Resolve(typeof(IDummy), NAME);

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
