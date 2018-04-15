using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
