using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.DependencyInjection.Attributes;
using Com.Ericmas001.DependencyInjection.Exceptions;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.RegistrantFinders;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;
using FluentAssertions;
using Xunit;

namespace Com.Ericmas001.DependencyInjection.Tests
{
    public class TypeFinderTests
    {
        public static readonly IRegisteredElement E01 = new SimpleRegisteredElement(typeof(string));
        public static readonly IRegisteredElement E02 = new SimpleRegisteredElement(typeof(string));
        public static readonly IRegisteredElement E03 = new SimpleRegisteredElement(typeof(string));
        public static readonly IRegisteredElement E04 = new SimpleRegisteredElement(typeof(string));
        public static readonly IRegisteredElement E05 = new SimpleRegisteredElement(typeof(string));
        public static readonly IRegisteredElement E06 = new SimpleRegisteredElement(typeof(string));
        public static readonly IRegisteredElement E07 = new SimpleRegisteredElement(typeof(string));
        public static readonly IRegisteredElement E08 = new SimpleRegisteredElement(typeof(string));
        public static readonly IRegisteredElement E09 = new SimpleRegisteredElement(typeof(string));

        private class R01 : IRegistrant
        {
            public IEnumerable<IRegisteredElement> GetRegisteredTypeAssociation() => new[] { E01, E02, E03 };
        }
        [MustRegisterAfter(typeof(R02Middle), typeof(R03))]
        private class R01Last : R01
        {
        }
        private class R02 : IRegistrant
        {
            public IEnumerable<IRegisteredElement> GetRegisteredTypeAssociation() => new[] { E04, E05, E06 };
        }

        [ManualRegistrant]
        private class R02Manual : R02
        {
        }
        [MustRegisterAfter(typeof(R03))]
        private class R02Middle : R02
        {
        }
        [MustRegisterAfter(typeof(R03Last))]
        private class R02Last : R02
        {
        }

        private class R03 : IRegistrant
        {
            public IEnumerable<IRegisteredElement> GetRegisteredTypeAssociation() => new[] { E07, E08, E09 };
        }
        [MustRegisterAfter(typeof(R02Last))]
        private class R03Last : R03
        {
        }

        private class SimpleTypesFinder : ITypesFinder
        {
            private readonly Type[] m_Registrants;

            public SimpleTypesFinder(params Type[] registrants)
            {
                m_Registrants = registrants;
            }
            public IEnumerable<Type> FindTypesImplementing<TInterface>()
            {
                return m_Registrants;
            }
        }

        [Fact]
        public void FindsAllRegistrant()
        {
            //Arrange
            var finder = new RegistrantFinderBuilder()
                .UsingTypeFinder(new SimpleTypesFinder(typeof(R01), typeof(R02), typeof(R03)))
                .Build();

            //Act
            var elements = finder.GetAllRegistrations().ToArray();

            //Assert
            elements.Length.Should().Be(9);
            elements[0].Should().Be(E01);
            elements[1].Should().Be(E02);
            elements[2].Should().Be(E03);
            elements[3].Should().Be(E04);
            elements[4].Should().Be(E05);
            elements[5].Should().Be(E06);
            elements[6].Should().Be(E07);
            elements[7].Should().Be(E08);
            elements[8].Should().Be(E09);
        }

        [Fact]
        public void IgnoreManuals()
        {
            //Arrange
            var finder = new RegistrantFinderBuilder()
                .UsingTypeFinder(new SimpleTypesFinder(typeof(R01), typeof(R02Manual), typeof(R03)))
                .Build();

            //Act
            var elements = finder.GetAllRegistrations().ToArray();

            //Assert
            elements.Length.Should().Be(6);
            elements[0].Should().Be(E01);
            elements[1].Should().Be(E02);
            elements[2].Should().Be(E03);
            elements[3].Should().Be(E07);
            elements[4].Should().Be(E08);
            elements[5].Should().Be(E09);
        }

        [Fact]
        public void IngoreMustBeAfterIfNotThere()
        {
            //Arrange
            var finder = new RegistrantFinderBuilder()
                .UsingTypeFinder(new SimpleTypesFinder(typeof(R01Last), typeof(R02Middle)))
                .Build();

            //Act
            var elements = finder.GetAllRegistrations().ToArray();

            //Assert
            elements.Length.Should().Be(6);
            elements[0].Should().Be(E04);
            elements[1].Should().Be(E05);
            elements[2].Should().Be(E06);
            elements[3].Should().Be(E01);
            elements[4].Should().Be(E02);
            elements[5].Should().Be(E03);
        }

        [Fact]
        public void RespectMustBeAfter()
        {
            //Arrange
            var finder = new RegistrantFinderBuilder()
                .UsingTypeFinder(new SimpleTypesFinder(typeof(R01Last), typeof(R02Middle), typeof(R03)))
                .Build();

            //Act
            var elements = finder.GetAllRegistrations().ToArray();

            //Assert
            elements.Length.Should().Be(9);
            elements[0].Should().Be(E07);
            elements[1].Should().Be(E08);
            elements[2].Should().Be(E09);
            elements[3].Should().Be(E04);
            elements[4].Should().Be(E05);
            elements[5].Should().Be(E06);
            elements[6].Should().Be(E01);
            elements[7].Should().Be(E02);
            elements[8].Should().Be(E03);
        }
        [Fact]
        public void CircularMustBeAfterIsBad()
        {
            //Arrange
            var finder = new RegistrantFinderBuilder()
                .UsingTypeFinder(new SimpleTypesFinder(typeof(R02Last), typeof(R03Last)))
                .Build();

            //Act
            Action action = () => finder.GetAllRegistrations();

            //Assert
            action.Should().Throw<CircularRegistrantDependencyException>();
        }
    }
}
