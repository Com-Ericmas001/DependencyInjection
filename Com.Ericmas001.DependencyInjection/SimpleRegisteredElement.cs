using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
{
    public class SimpleRegisteredElement : IRegisteredElement
    {
        public Type RegisteredType { get; set; }

        public SimpleRegisteredElement(Type registeredType)
        {
            RegisteredType = registeredType;
        }
    }
}
