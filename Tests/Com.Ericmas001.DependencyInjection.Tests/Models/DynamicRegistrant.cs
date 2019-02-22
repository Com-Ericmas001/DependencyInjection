using System;
using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Registrants;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;

namespace Com.Ericmas001.DependencyInjection.Tests.Models
{
    public class DynamicRegistrant : AbstractRegistrant
    {
        private readonly Action<DynamicRegistrant> _registerFnct;

        public DynamicRegistrant(Action<DynamicRegistrant> registerFnct)
        {
            _registerFnct = registerFnct;
        }
        protected override void RegisterEverything()
        {
            _registerFnct(this);
        }

        public new void AddToRegistrant(IRegisteredElement elem)
        {
            base.AddToRegistrant(elem);
        }
        public new void AddToRegistrant(IEnumerable<IRegisteredElement> elems)
        {
            base.AddToRegistrant(elems);
        }
        public new void AddToRegistrant(IRegistrant registrant)
        {
            base.AddToRegistrant(registrant);
        }
        public new void AddToRegistrant<T>() where T : IRegistrant, new()
        {
            base.AddToRegistrant<T>();
        }
        public new void Register<T>()
        {
            base.Register<T>();
        }
        public new void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            base.Register<TInterface, TImplementation>();
        }
        public new void Register<TInterface, TImplementation>(string name)
            where TImplementation : TInterface
        {
            base.Register<TInterface, TImplementation>(name);
        }
        public new void Register<T>(Func<T> factory)
            where T : class
        {
            base.Register(factory);
        }
        public new void Register<TInterface, TImplementation>(Func<TImplementation> factory)
            where TImplementation : class, TInterface
        {
            base.Register<TInterface, TImplementation>(factory);
        }
        public new void Register<TInterface, TImplementation>(string name, Func<TImplementation> factory)
            where TImplementation : class, TInterface
        {
            base.Register<TInterface, TImplementation>(name, factory);
        }
        public new void RegisterInstance<TInterface>(TInterface obj)
        {
            base.RegisterInstance(obj);
        }
    }
}
