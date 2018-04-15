using System;

namespace Com.Ericmas001.DependencyInjection
{
    public interface IResolverService
    {
        T Resolve<T>();
        object Resolve(Type t);
    }
}
