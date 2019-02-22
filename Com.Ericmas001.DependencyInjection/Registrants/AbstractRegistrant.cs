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
        protected void Register<T>()
        {
            _registeredTypeAssociation.Add(new SimpleRegisteredElement(typeof(T)));
        }
        protected void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _registeredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)));
        }
        protected void Register<TInterface, TImplementation>(string name)
            where TImplementation : TInterface
        {
            _registeredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)) { Name = name });
        }
        protected void Register<T>(Func<T> factory) 
            where T:class
        {
            _registeredTypeAssociation.Add(new SimpleRegisteredElement(typeof(T)){Factory = factory});
        }
        protected void Register<TInterface, TImplementation>(Func<TImplementation> factory)
            where TImplementation : class, TInterface
        {
            _registeredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)) { Factory = factory });
        }
        protected void Register<TInterface, TImplementation>(string name, Func<TImplementation> factory)
            where TImplementation : class, TInterface
        {
            _registeredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)) { Name = name, Factory = factory });
        }
        protected void RegisterInstance<TInterface>(TInterface obj)
        {
            _registeredTypeAssociation.Add(new InstanceImplementationRegisteredElement(typeof(TInterface), obj));
        }
    }
}
