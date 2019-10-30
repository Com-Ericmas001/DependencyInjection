using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Unity;
using Unity.Lifetime;

namespace Com.Ericmas001.DependencyInjection.Unity
{
    public static class UnityHelper
    {
        public static void RegisterTypes(this IRegistrant registrant, IUnityContainer container, IResolverService resolverService)
        {
            registrant.GetRegisteredTypeAssociation().RegisterTypes(container, resolverService);
        }
        public static void RegisterTypes(this IEnumerable<IRegisteredElement> elements, IUnityContainer container, IResolverService resolver)
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
                                container.RegisterType(iElem.RegisteredType, iElem.ImplementationType, iElem.IsSingleton ? (ITypeLifetimeManager)new ContainerControlledLifetimeManager() : new TransientLifetimeManager());
                            else
                                container.RegisterType(iElem.RegisteredType, iElem.ImplementationType, iElem.Name, iElem.IsSingleton ? (ITypeLifetimeManager)new ContainerControlledLifetimeManager() : new TransientLifetimeManager());
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(iElem.Name))
                                container.RegisterFactory(iElem.RegisteredType, c => iElem.Factory(resolver), iElem.IsSingleton ? (IFactoryLifetimeManager)new ContainerControlledLifetimeManager() : new TransientLifetimeManager());
                            else
                                container.RegisterFactory(iElem.RegisteredType, iElem.Name, c => iElem.Factory(resolver), iElem.IsSingleton ? (IFactoryLifetimeManager)new ContainerControlledLifetimeManager() : new TransientLifetimeManager());
                        }

                        break;
                    }
                    case SimpleRegisteredElement sElem:
                    {
                        if (sElem.Factory == null)
                            container.RegisterType(sElem.RegisteredType, sElem.IsSingleton ? (ITypeLifetimeManager)new ContainerControlledLifetimeManager() : new TransientLifetimeManager());
                        else
                            container.RegisterFactory(sElem.RegisteredType, c => sElem.Factory(resolver), sElem.IsSingleton ? (IFactoryLifetimeManager)new ContainerControlledLifetimeManager() : new TransientLifetimeManager());
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
