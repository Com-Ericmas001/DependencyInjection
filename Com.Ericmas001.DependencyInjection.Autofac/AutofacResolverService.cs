﻿using System;
using Autofac;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;

namespace Com.Ericmas001.DependencyInjection.Autofac
{
    public class AutofacResolverService : IResolverService
    {
        public ILifetimeScope Scope { get; set; }

        public T Resolve<T>()
        {
            return Scope.Resolve<T>();
        }

        public object Resolve(Type t)
        {
            return Scope.Resolve(t);
        }
    }
}
