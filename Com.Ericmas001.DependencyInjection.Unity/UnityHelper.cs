﻿using System.Collections.Generic;
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
