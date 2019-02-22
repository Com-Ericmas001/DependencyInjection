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
                    case InstanceImplementationRegisteredElement iiElem:
                    {
                        container.RegisterInstance(iiElem.RegisteredType, iiElem.Instance);
                        break;
                    }
                    case ImplementationRegisteredElement iElem:
                    {
                        if (iElem.Factory == null)
                        {
                            if (string.IsNullOrEmpty(iElem.Name))
                                container.RegisterType(iElem.RegisteredType, iElem.ImplementationType);
                            else
                                container.RegisterType(iElem.RegisteredType, iElem.ImplementationType, iElem.Name);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(iElem.Name))
                                container.RegisterFactory(iElem.RegisteredType, c => iElem.Factory());
                            else
                                container.RegisterFactory(iElem.RegisteredType, iElem.Name, c => iElem.Factory());
                        }

                        break;
                    }
                    case SimpleRegisteredElement sElem:
                    {
                        if(sElem.Factory == null)
                            container.RegisterType(sElem.RegisteredType);
                        else
                            container.RegisterFactory(sElem.RegisteredType, c => sElem.Factory());
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
