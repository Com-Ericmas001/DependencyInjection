using System;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;

namespace Com.Ericmas001.DependencyInjection.RegisteredElements
{
    public class SimpleRegisteredElement : IRegisteredElement
    {
        public Type RegisteredType { get; set; }
        public Func<IResolverService, object> Factory { get; set; }
        public bool IsSingleton { get; set; } = false;

        public SimpleRegisteredElement(Type registeredType)
        {
            RegisteredType = registeredType;
        }
    }
}
