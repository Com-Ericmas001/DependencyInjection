using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Microsoft.Tests
{
    public class MicrosoftResolverServiceTest
    {
        [Fact]
        public void TestGenericResolve()
        {
            //Arrange
            IServiceCollection container = new ServiceCollection();
            container.AddTransient<Dummy>();
            var provider = container.BuildServiceProvider();
            var resolver = new MicrosoftResolverService(){Provider = provider };

            //Act
            var res = resolver.Resolve<Dummy>();

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
        [Fact]
        public void TestTypeResolve()
        {
            //Arrange
            IServiceCollection container = new ServiceCollection();
            container.AddTransient<Dummy>();
            var provider = container.BuildServiceProvider();
            var resolver = new MicrosoftResolverService() { Provider = provider };

            //Act
            var res = resolver.Resolve(typeof(Dummy));

            //Assert
            res.Should().NotBeNull().And.BeOfType<Dummy>();
        }
    }
}
