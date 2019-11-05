using System;
using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.RegisteredElements;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ericmas001.DependencyInjection.Microsoft
{
    public static class MicrosoftHelper
    {
        public static void RegisterTypes(this IRegistrant registrant, IServiceCollection container, IResolverService resolver)
        {
            registrant.GetRegisteredTypeAssociation().RegisterTypes(container, resolver);
        }
        public static void RegisterTypes(this IEnumerable<IRegisteredElement> elements, IServiceCollection container, IResolverService resolver)
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
                                throw new NotSupportedException($"Naming registrations are not supported. {iElem.RegisteredType.FullName}({iElem.Name}) -> {iElem.ImplementationType.FullName}")
                                {
                                    HelpLink = "https://github.com/aspnet/DependencyInjection/issues/473"
                                };
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(iElem.Name))
                                if (iElem.IsSingleton)
                                    container.AddSingleton(iElem.RegisteredType, c => iElem.Factory(resolver));
                                else
                                    container.AddTransient(iElem.RegisteredType, c => iElem.Factory(resolver));
                            else
                                throw new NotSupportedException($"Naming registrations are not supported. {iElem.RegisteredType.FullName}({iElem.Name}) -> [FACTORY]")
                                {
                                    HelpLink = "https://github.com/aspnet/DependencyInjection/issues/473"
                                };
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
                            container.AddSingleton(sElem.RegisteredType, c => sElem.Factory(resolver));
                        else
                            container.AddTransient(sElem.RegisteredType, c => sElem.Factory(resolver));
                        break;
                    }
                    default:
                    {
                        container.AddTransient(elem.RegisteredType);
                        break;
                    }
                }
            }
        }
    }
}
