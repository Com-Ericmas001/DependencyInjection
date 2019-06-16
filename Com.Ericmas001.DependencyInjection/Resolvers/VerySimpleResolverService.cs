using System;
using System.Reflection;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;

namespace Com.Ericmas001.DependencyInjection.Resolvers
{
    public class VerySimpleResolverService : IResolverService
    {
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public T Resolve<T>(string name)
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type t)
        {
            ConstructorInfo ctor = t.GetConstructor(new Type[0]);
            return ctor?.Invoke(new object[0]);
        }

        public object Resolve(Type t, string name)
        {
            return Resolve(t);
        }
    }
}
