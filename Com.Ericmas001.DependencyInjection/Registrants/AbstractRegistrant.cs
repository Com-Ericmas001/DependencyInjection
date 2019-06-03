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
        private readonly List<IRegisteredElement> _registeredTypeAssociation = new List<IRegisteredElement>();

        protected abstract void RegisterEverything();

        public IEnumerable<IRegisteredElement> GetRegisteredTypeAssociation()
        {
            _registeredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(IResolverService), typeof(VerySimpleResolverService)));
            RegisterEverything();
            return _registeredTypeAssociation.ToArray();
        }
        protected void AddToRegistrant(IRegisteredElement elem)
        {
            _registeredTypeAssociation.Add(elem);
        }
        protected void AddToRegistrant(IEnumerable<IRegisteredElement> elems)
        {
            _registeredTypeAssociation.AddRange(elems);
        }
        protected void AddToRegistrant(IRegistrant registrant)
        {
            AddToRegistrant(registrant.GetRegisteredTypeAssociation());
        }
        protected void AddToRegistrant<T>() where T : IRegistrant, new()
        {
            AddToRegistrant(new T());
        }

        protected void Register<T>(Func<IResolverService, T> factory = null, bool isSingleton = false)
            where T : class
        {
            AddToRegistrant(new SimpleRegisteredElement(typeof(T)) { Factory = factory, IsSingleton = isSingleton });
        }

        protected void Register<TInterface, TImplementation>(string name = null, Func<IResolverService, TImplementation> factory = null, bool isSingleton = false)
            where TImplementation : class, TInterface
        {
            AddToRegistrant(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)) { Name = name, IsSingleton = isSingleton, Factory = factory });
        }

        protected void RegisterInstance<TInterface>(TInterface obj)
        {
            AddToRegistrant(new InstanceImplementationRegisteredElement(typeof(TInterface), obj));
        }
    }
}
