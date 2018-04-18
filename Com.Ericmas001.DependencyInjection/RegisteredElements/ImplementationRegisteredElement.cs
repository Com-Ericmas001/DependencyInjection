﻿using System;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;

namespace Com.Ericmas001.DependencyInjection.RegisteredElements
{
    public class ImplementationRegisteredElement : IRegisteredElement
    {
        public Type RegisteredType { get; set; }
        public Type ImplementationType { get; set; }

        public ImplementationRegisteredElement(Type registeredType, Type implementationType)
        {
            RegisteredType = registeredType;
            ImplementationType = implementationType;
        }
    }
}