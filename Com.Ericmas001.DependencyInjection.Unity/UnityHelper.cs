using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Unity;

namespace Com.Ericmas001.DependencyInjection.Unity
{
    public static class UnityHelper
    {
        public static void RegisterTypes(this IRegistrant registrant, IUnityContainer container)
        {
            registrant.GetRegisteredTypeAssociation().RegisterTypes(container);
        }
        public static void RegisterTypes(this IEnumerable<IRegisteredElement> elements, IUnityContainer container)
        {
            foreach (var elem in elements)
            {
                switch (elem)
                {
                    case InstanceRegisteredElement iElem:
                    {
                        container.RegisterInstance(iElem.Instance);
                        break;
                    }
                    case InstanceImplementationRegisteredElement iiElem:
                    {
                        container.RegisterInstance(iiElem.RegisteredType, iiElem.Instance);
                        break;
                    }
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
                    case SimpleRegisteredElement sElem:
                    {
                        container.RegisterType(sElem.RegisteredType);
                        break;
                    }
                    default:
                    {
                        container.RegisterType(elem.RegisteredType);
                        break;
                    }
                }
            }
            container.RegisterInstance<IResolverService>(new UnityResolverService(container));
        }
    }
}
