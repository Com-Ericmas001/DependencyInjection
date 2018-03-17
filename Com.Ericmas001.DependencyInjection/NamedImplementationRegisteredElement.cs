using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
{
    public class NamedImplementationRegisteredElement : ImplementationRegisteredElement
    {
        public string Name { get; set; }

        public NamedImplementationRegisteredElement(Type registeredType, Type implementationType, string name) : base(registeredType,implementationType)
        {
            Name = name;
        }
    }
}
