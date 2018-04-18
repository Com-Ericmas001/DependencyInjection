using System;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Unity;

namespace Com.Ericmas001.DependencyInjection.Unity
{
    public class UnityResolverService : IResolverService
    {
        private readonly IUnityContainer m_Container;

        public UnityResolverService(IUnityContainer container)
        {
            m_Container = container;
        }

        public T Resolve<T>()
        {
            return m_Container.Resolve<T>();
        }

        public object Resolve(Type t)
        {
            return m_Container.Resolve(t);
        }
    }
}
