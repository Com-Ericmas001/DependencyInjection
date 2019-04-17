using System;
using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ericmas001.DependencyInjection.Microsoft
{
    public static class UnityHelper
    {
        public static void RegisterTypes(this IRegistrant registrant, IServiceCollection container)
        {
            registrant.GetRegisteredTypeAssociation().RegisterTypes(container);
        }
        public static void RegisterTypes(this IEnumerable<IRegisteredElement> elements, IServiceCollection container)
        {
            foreach (var elem in elements)
            {
                switch (elem)
                {
                    case InstanceImplementationRegisteredElement iiElem:
                    {
                        container.AddSingleton(iiElem.RegisteredType, iiElem.Instance);
                        break;
                    }
                    case ImplementationRegisteredElement iElem:
                    {
                        if (iElem.Factory == null)
                        {
                            if (string.IsNullOrEmpty(iElem.Name))
                                if (iElem.IsSingleton)
                                    container.AddSingleton(iElem.RegisteredType, iElem.ImplementationType);
                                else
                                    container.AddTransient(iElem.RegisteredType, iElem.ImplementationType);
                            else
                                throw new NotImplementedException("https://github.com/aspnet/DependencyInjection/issues/473");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(iElem.Name))
                                if (iElem.IsSingleton)
                                    container.AddSingleton(iElem.RegisteredType, c => iElem.Factory());
                                else
                                    container.AddTransient(iElem.RegisteredType, c => iElem.Factory());
                            else
                                throw new NotImplementedException("https://github.com/aspnet/DependencyInjection/issues/473");
                        }

                        break;
                    }
                    case SimpleRegisteredElement sElem:
                    {
                        if (sElem.Factory == null)
                            if (sElem.IsSingleton)
                                container.AddSingleton(sElem.RegisteredType);
                            else
                                container.AddTransient(sElem.RegisteredType);
                        else if (sElem.IsSingleton)
                            container.AddSingleton(sElem.RegisteredType, c => sElem.Factory());
                        else
                            container.AddTransient(sElem.RegisteredType, c => sElem.Factory());
                        break;
                    }
                    default:
                    {
                        container.AddTransient(elem.RegisteredType);
                        break;
                    }
                }
            }
            container.AddSingleton<IResolverService>(new MicrosoftResolverService());
        }

        public static void SetProvider(IServiceProvider provider)
        {
            ((MicrosoftResolverService)provider.GetService<IResolverService>()).Provider = provider;
        }
    }
}
