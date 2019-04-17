using System;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;

namespace Com.Ericmas001.DependencyInjection.RegisteredElements
{
    public class ImplementationRegisteredElement : IRegisteredElement
    {
        public Type RegisteredType { get; set; }
        public Type ImplementationType { get; set; }
        public string Name { get; set; }
        public Func<object> Factory { get; set; }
        public bool IsSingleton { get; set; } = false;

        public ImplementationRegisteredElement(Type registeredType, Type implementationType)
        {
            RegisteredType = registeredType;
            ImplementationType = implementationType;
        }
    }
}
