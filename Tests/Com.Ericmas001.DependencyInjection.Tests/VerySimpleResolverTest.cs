using Com.Ericmas001.DependencyInjection.Resolvers;
using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Tests
{
    public class VerySimpleResolverTest
    {
        [Fact]
        public void TestGenericResolve()
        {
            //Arrange
            var resolver = new VerySimpleResolverService();

            //Act
            var res = resolver.Resolve<Dummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestTypeResolve()
        {
            //Arrange
            var resolver = new VerySimpleResolverService();

            //Act
            var res = resolver.Resolve(typeof(Dummy));

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
