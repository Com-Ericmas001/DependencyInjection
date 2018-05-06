using System;
using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;
using Com.Ericmas001.DependencyInjection.Resolvers;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;

namespace Com.Ericmas001.DependencyInjection.Registrants
{
    public abstract class AbstractRegistrant : IRegistrant
    {
        private readonly List<IRegisteredElement> m_RegisteredTypeAssociation = new List<IRegisteredElement>();

        protected abstract void RegisterEverything();

        public IEnumerable<IRegisteredElement> GetRegisteredTypeAssociation()
        {
            m_RegisteredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(IResolverService), typeof(VerySimpleResolverService)));
            RegisterEverything();
            return m_RegisteredTypeAssociation.ToArray();
        }
        protected void AddToRegistrant(IRegisteredElement elem)
        {
            m_RegisteredTypeAssociation.Add(elem);
        }
        protected void AddToRegistrant(IEnumerable<IRegisteredElement> elems)
        {
            m_RegisteredTypeAssociation.AddRange(elems);
        }
        protected void AddToRegistrant(IRegistrant registrant)
        {
            AddToRegistrant(registrant.GetRegisteredTypeAssociation());
        }
        protected void AddToRegistrant<T>() where T : IRegistrant, new()
        {
            AddToRegistrant(new T());
        }
        protected void Register<T>()
        {
            m_RegisteredTypeAssociation.Add(new SimpleRegisteredElement(typeof(T)));
        }
        protected void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            m_RegisteredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)));
        }
        protected void Register<TInterface, TImplementation>(string name)
            where TImplementation : TInterface
        {
            m_RegisteredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)) { Name = name });
        }
        protected void Register<T>(Func<T> factory) 
            where T:class
        {
            m_RegisteredTypeAssociation.Add(new SimpleRegisteredElement(typeof(T)){Factory = factory});
        }
        protected void Register<TInterface, TImplementation>(Func<TImplementation> factory)
            where TImplementation : class, TInterface
        {
            m_RegisteredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)) { Factory = factory });
        }
        protected void Register<TInterface, TImplementation>(string name, Func<TImplementation> factory)
            where TImplementation : class, TInterface
        {
            m_RegisteredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)) { Name = name, Factory = factory });
        }
        protected void RegisterInstance<TInterface>(TInterface obj)
        {
            m_RegisteredTypeAssociation.Add(new InstanceImplementationRegisteredElement(typeof(TInterface), obj));
        }
    }
}
