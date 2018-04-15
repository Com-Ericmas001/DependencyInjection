using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
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
