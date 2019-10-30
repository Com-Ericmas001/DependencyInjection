using Autofac;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using System.Collections.Generic;

namespace Com.Ericmas001.DependencyInjection.Autofac
{
    public static class AutofacHelper
    {
        public static void RegisterTypes(this IRegistrant registrant, ContainerBuilder container, IResolverService resolverService)
        {
            registrant.GetRegisteredTypeAssociation().RegisterTypes(container, resolverService);
        }
        public static void RegisterTypes(this IEnumerable<IRegisteredElement> elements, ContainerBuilder container, IResolverService resolverService)
        {
            foreach (var elem in elements)
            {
                switch (elem)
                {
                    case InstanceImplementationRegisteredElement iiElem:
                    {
                        container.RegisterInstance(iiElem.Instance).As(iiElem.RegisteredType);
                        break;
                    }
                    case ImplementationRegisteredElement iElem:
                    {
                        if (iElem.Factory == null)
                        {
                            if (string.IsNullOrEmpty(iElem.Name))
                                if (iElem.IsSingleton)
                                    container.RegisterType(iElem.ImplementationType).As(iElem.RegisteredType).SingleInstance();
                                else
                                    container.RegisterType(iElem.ImplementationType).As(iElem.RegisteredType);
                            else if (iElem.IsSingleton)
                                container.RegisterType(iElem.ImplementationType).Named(iElem.Name, iElem.RegisteredType).SingleInstance();
                            else
                                container.RegisterType(iElem.ImplementationType).Named(iElem.Name, iElem.RegisteredType);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(iElem.Name))
                                if (iElem.IsSingleton)
                                    container.Register(c => iElem.Factory(resolverService)).As(iElem.RegisteredType).SingleInstance();
                                else
                                    container.Register(c => iElem.Factory(resolverService)).As(iElem.RegisteredType);
                            else if (iElem.IsSingleton)
                                container.Register(c => iElem.Factory(resolverService)).Named(iElem.Name, iElem.RegisteredType).SingleInstance();
                            else
                                container.Register(c => iElem.Factory(resolverService)).Named(iElem.Name, iElem.RegisteredType);
                        }

                        break;
                    }
                    case SimpleRegisteredElement sElem:
                    {
                        if (sElem.Factory == null)
                            if (sElem.IsSingleton)
                                container.RegisterType(sElem.RegisteredType).SingleInstance();
                            else
                                container.RegisterType(sElem.RegisteredType);
                        else if (sElem.IsSingleton)
                            container.Register(c => sElem.Factory(resolverService)).As(sElem.RegisteredType).SingleInstance();
                        else
                            container.Register(c => sElem.Factory(resolverService)).As(sElem.RegisteredType);
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
