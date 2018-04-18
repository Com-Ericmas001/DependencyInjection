using System;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;

namespace Com.Ericmas001.DependencyInjection.RegisteredElements
{
    public class InstanceRegisteredElement : IRegisteredElement
    {
        public Type RegisteredType => Instance.GetType();
        public object Instance { get; set; }

        public InstanceRegisteredElement(object instance)
        {
            Instance = instance;
        }
    }
}
