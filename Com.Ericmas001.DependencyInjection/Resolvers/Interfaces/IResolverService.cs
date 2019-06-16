using System;

namespace Com.Ericmas001.DependencyInjection.Resolvers.Interfaces
{
    public interface IResolverService
    {
        T Resolve<T>();
        T Resolve<T>(string name);
        object Resolve(Type t);
        object Resolve(Type t, string name);
    }
}
