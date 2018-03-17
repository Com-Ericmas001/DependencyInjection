using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Com.Ericmas001.DependencyInjection.Unity
{
    public static class UnityHelper
    {
        public static void RegisterTypes(IUnityContainer container, IEnumerable<IRegisteredElement> elements)
        {
            foreach (var elem in elements)
            {
                switch (elem)
                {
                    case NamedImplementationRegisteredElement niElem:
                    {
                        container.RegisterType(niElem.RegisteredType, niElem.ImplementationType, niElem.Name);
                        break;
                    }
                    case ImplementationRegisteredElement iElem:
                    {
                        container.RegisterType(iElem.RegisteredType, iElem.ImplementationType);
                        break;
                    }
                    default:
                    {
                        container.RegisterType(elem.RegisteredType);
                        break;
                    }
                }
            }
        }
    }
}
