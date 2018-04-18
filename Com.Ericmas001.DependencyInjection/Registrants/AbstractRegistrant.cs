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
            m_RegisteredTypeAssociation.Add(new NamedImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation), name));
        }
        protected void RegisterInstance(object obj)
        {
            m_RegisteredTypeAssociation.Add(new InstanceRegisteredElement(obj));
        }
        protected void RegisterInstance<TInterface>(TInterface obj)
        {
            m_RegisteredTypeAssociation.Add(new InstanceImplementationRegisteredElement(typeof(TInterface), obj));
        }
    }
}
