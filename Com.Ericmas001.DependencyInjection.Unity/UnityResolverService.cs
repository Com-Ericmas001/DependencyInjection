﻿using System;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Unity;

namespace Com.Ericmas001.DependencyInjection.Unity
{
    public class UnityResolverService : IResolverService
    {
        private readonly IUnityContainer _container;

        public UnityResolverService(IUnityContainer container)
        {
            _container = container;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        public object Resolve(Type t)
        {
            return _container.Resolve(t);
        }

        public object Resolve(Type t, string name)
        {
            return _container.Resolve(t, name);
        }
    }
}
