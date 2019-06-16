using System;
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

        public T Resolve<T>(string name)
        {
            return Scope.ResolveNamed<T>(name);
        }

        public object Resolve(Type t)
        {
            return Scope.Resolve(t);
        }

        public object Resolve(Type t, string name)
        {
            return Scope.ResolveNamed(name, t);
        }
    }
}
