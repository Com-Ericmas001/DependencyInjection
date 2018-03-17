using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
{
    public class ImplementationRegisteredElement : IRegisteredElement
    {
        public Type RegisteredType { get; set; }
        public Type ImplementationType { get; set; }

        public ImplementationRegisteredElement(Type registeredType, Type implementationType)
        {
            RegisteredType = registeredType;
            ImplementationType = implementationType;
        }
    }
}
