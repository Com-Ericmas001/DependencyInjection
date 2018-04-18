using System;

namespace Com.Ericmas001.DependencyInjection.RegisteredElements
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
