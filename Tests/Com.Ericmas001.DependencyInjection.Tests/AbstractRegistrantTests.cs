using System.Linq;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Com.Ericmas001.DependencyInjection.Tests.Models;
using FluentAssertions;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Tests
{
    public class AbstractRegistrantTests
    {
        [Fact]
        public void TestEmptyContainsOnlyResolver()
        {
            //Arrange
            var reg = new DynamicRegistrant(r => { });

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            res.Single().RegisteredType.Should().Be<IResolverService>();
        }
        [Fact]
        public void TestEmptyIsEmpty()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r => { });

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().BeEmpty();
        }
        [Fact]
        public void TestSimpleRegister()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<Dummy>());

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<SimpleRegisteredElement>().And.HaveCount(1);
            var elem = (SimpleRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<Dummy>();
            elem.Factory.Should().BeNull();
            elem.IsSingleton.Should().BeFalse();
        }
        [Fact]
        public void TestSimpleRegisterSingleton()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<Dummy>(isSingleton: true));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<SimpleRegisteredElement>().And.HaveCount(1);
            var elem = (SimpleRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<Dummy>();
            elem.Factory.Should().BeNull();
            elem.IsSingleton.Should().BeTrue();
        }
        [Fact]
        public void TestImplementationRegister()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<IDummy, Dummy>());

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (ImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.ImplementationType.Should().Be<Dummy>();
            elem.Name.Should().BeNullOrEmpty();
            elem.Factory.Should().BeNull();
            elem.IsSingleton.Should().BeFalse();
        }
        [Fact]
        public void TestImplementationRegisterSingleton()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<IDummy, Dummy>(isSingleton: true));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (ImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.ImplementationType.Should().Be<Dummy>();
            elem.Name.Should().BeNullOrEmpty();
            elem.Factory.Should().BeNull();
            elem.IsSingleton.Should().BeTrue();
        }
        [Fact]
        public void TestImplementationRegisterNamed()
        {
            //Arrange
            const string NAME = "MyName";
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<IDummy, Dummy>(NAME));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (ImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.ImplementationType.Should().Be<Dummy>();
            elem.Name.Should().Be(NAME);
            elem.Factory.Should().BeNull();
            elem.IsSingleton.Should().BeFalse();
        }
        [Fact]
        public void TestImplementationRegisterNamedSingleton()
        {
            //Arrange
            const string NAME = "MyName";
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<IDummy, Dummy>(NAME, isSingleton: true));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (ImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.ImplementationType.Should().Be<Dummy>();
            elem.Name.Should().Be(NAME);
            elem.Factory.Should().BeNull();
            elem.IsSingleton.Should().BeTrue();
        }
        [Fact]
        public void TestSimpleRegisterWithFactory()
        {
            //Arrange
            Dummy CreatFunc(IResolverService resolver) => new Dummy();
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register(CreatFunc));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<SimpleRegisteredElement>().And.HaveCount(1);
            var elem = (SimpleRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<Dummy>();
            elem.IsSingleton.Should().BeFalse();
            //elem.Factory.Should().Be((Func<Dummy>) CreatFunc); Todo: Check that !
        }
        [Fact]
        public void TestSimpleRegisterWithFactorySingleton()
        {
            //Arrange
            Dummy CreatFunc(IResolverService resolver) => new Dummy();
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register(CreatFunc, true));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<SimpleRegisteredElement>().And.HaveCount(1);
            var elem = (SimpleRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<Dummy>();
            elem.IsSingleton.Should().BeTrue();
            //elem.Factory.Should().Be((Func<Dummy>) CreatFunc); Todo: Check that !
        }
        [Fact]
        public void TestImplementationRegisterWithFactory()
        {
            //Arrange
            Dummy CreatFunc(IResolverService resolver) => new Dummy();
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<IDummy, Dummy>(factory: CreatFunc));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (ImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.ImplementationType.Should().Be<Dummy>();
            elem.Name.Should().BeNullOrEmpty();
            elem.IsSingleton.Should().BeFalse();
            //elem.Factory.Should().Be((Func<Dummy>)CreatFunc); Todo: Check that !
        }
        [Fact]
        public void TestImplementationRegisterWithFactorySingleton()
        {
            //Arrange
            Dummy CreatFunc(IResolverService resolver) => new Dummy();
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<IDummy, Dummy>(factory: CreatFunc, isSingleton: true));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (ImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.ImplementationType.Should().Be<Dummy>();
            elem.Name.Should().BeNullOrEmpty();
            elem.IsSingleton.Should().BeTrue();
            //elem.Factory.Should().Be((Func<Dummy>)CreatFunc); Todo: Check that !
        }
        [Fact]
        public void TestImplementationRegisterNamedWithFactory()
        {
            //Arrange
            const string NAME = "MyName";
            Dummy CreatFunc(IResolverService resolver) => new Dummy();
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<IDummy, Dummy>(NAME, CreatFunc));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (ImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.ImplementationType.Should().Be<Dummy>();
            elem.Name.Should().Be(NAME);
            elem.IsSingleton.Should().BeFalse();
            //elem.Factory.Should().Be((Func<Dummy>)CreatFunc); Todo: Check that !
        }
        [Fact]
        public void TestImplementationRegisterNamedWithFactorySingleton()
        {
            //Arrange
            const string NAME = "MyName";
            Dummy CreatFunc(IResolverService resolver) => new Dummy();
            var reg = new DynamicRegistrantWithoutResolver(r => r.Register<IDummy, Dummy>(NAME, CreatFunc, true));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<ImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (ImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.ImplementationType.Should().Be<Dummy>();
            elem.Name.Should().Be(NAME);
            elem.IsSingleton.Should().BeTrue();
            //elem.Factory.Should().Be((Func<Dummy>)CreatFunc); Todo: Check that !
        }
        [Fact]
        public void TestRegisterInstance()
        {
            //Arrange
            Dummy instance = new Dummy();
            var reg = new DynamicRegistrantWithoutResolver(r => r.RegisterInstance(instance));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<InstanceImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (InstanceImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<Dummy>();
            elem.Instance.Should().Be(instance);
        }
        [Fact]
        public void TestRegisterInstanceImplementingInterface()
        {
            //Arrange
            Dummy instance = new Dummy();
            var reg = new DynamicRegistrantWithoutResolver(r => r.RegisterInstance<IDummy>(instance));

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<InstanceImplementationRegisteredElement>().And.HaveCount(1);
            var elem = (InstanceImplementationRegisteredElement)res.Single();
            elem.RegisteredType.Should().Be<IDummy>();
            elem.Instance.Should().Be(instance);
        }
        [Fact]
        public void TestAddingRegistrantElement()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r =>
            {
                r.Register<Dummy>();
                r.AddToRegistrant(new SimpleRegisteredElement(typeof(BetterDummy)));
            });

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<SimpleRegisteredElement>().And.HaveCount(2);
            var elem1 = (SimpleRegisteredElement)res.First();
            elem1.RegisteredType.Should().Be<Dummy>();
            elem1.Factory.Should().BeNull();
            elem1.IsSingleton.Should().BeFalse();
            var elem2 = (SimpleRegisteredElement)res.Last();
            elem2.RegisteredType.Should().Be<BetterDummy>();
            elem2.Factory.Should().BeNull();
            elem2.IsSingleton.Should().BeFalse();
        }
        [Fact]
        public void TestAddingRegistrantElements()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r =>
            {
                r.Register<Dummy>();
                r.AddToRegistrant(new IRegisteredElement[] { new SimpleRegisteredElement(typeof(BetterDummy)) });
            });

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<SimpleRegisteredElement>().And.HaveCount(2);
            var elem1 = (SimpleRegisteredElement)res.First();
            elem1.RegisteredType.Should().Be<Dummy>();
            elem1.Factory.Should().BeNull();
            elem1.IsSingleton.Should().BeFalse();
            var elem2 = (SimpleRegisteredElement)res.Last();
            elem2.RegisteredType.Should().Be<BetterDummy>();
            elem2.Factory.Should().BeNull();
            elem2.IsSingleton.Should().BeFalse();
        }
        [Fact]
        public void TestAddingRegistrant()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r =>
            {
                r.Register<Dummy>();
                r.AddToRegistrant(new DynamicRegistrantWithoutResolver(r2 => r2.Register<BetterDummy>()));
            });

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<SimpleRegisteredElement>().And.HaveCount(2);
            var elem1 = (SimpleRegisteredElement)res.First();
            elem1.RegisteredType.Should().Be<Dummy>();
            elem1.Factory.Should().BeNull();
            elem1.IsSingleton.Should().BeFalse();
            var elem2 = (SimpleRegisteredElement)res.Last();
            elem2.RegisteredType.Should().Be<BetterDummy>();
            elem2.Factory.Should().BeNull();
            elem2.IsSingleton.Should().BeFalse();
        }
        [Fact]
        public void TestAddingRegistrantAutoConstruct()
        {
            //Arrange
            var reg = new DynamicRegistrantWithoutResolver(r =>
            {
                r.Register<Dummy>();
                r.AddToRegistrant<BetterDummyRegistrant>();
            });

            //Act
            var res = reg.GetRegisteredTypeAssociation().ToArray();

            //Assert
            res.Should().AllBeOfType<SimpleRegisteredElement>().And.HaveCount(2);
            var elem1 = (SimpleRegisteredElement)res.First();
            elem1.RegisteredType.Should().Be<Dummy>();
            elem1.Factory.Should().BeNull();
            elem1.IsSingleton.Should().BeFalse();
            var elem2 = (SimpleRegisteredElement)res.Last();
            elem2.RegisteredType.Should().Be<BetterDummy>();
            elem2.Factory.Should().BeNull();
            elem2.IsSingleton.Should().BeFalse();
        }
    }
}
