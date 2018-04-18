using System;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;

namespace Com.Ericmas001.DependencyInjection.RegisteredElements
{
    public class InstanceImplementationRegisteredElement : IRegisteredElement
    {
        public Type RegisteredType { get; set; }
        public object Instance { get; set; }

        public InstanceImplementationRegisteredElement(Type registeredType, object instance)
        {
            RegisteredType = registeredType;
            Instance = instance;
        }
    }
}
