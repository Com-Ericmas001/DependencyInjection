using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
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
