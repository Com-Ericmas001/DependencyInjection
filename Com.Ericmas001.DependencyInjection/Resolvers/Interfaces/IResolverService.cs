using System;

namespace Com.Ericmas001.DependencyInjection.Resolvers.Interfaces
{
    public interface IResolverService
    {
        T Resolve<T>();
        object Resolve(Type t);
    }
}
