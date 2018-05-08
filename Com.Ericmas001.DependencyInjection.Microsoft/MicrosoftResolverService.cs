using System;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ericmas001.DependencyInjection.Microsoft
{
    public class MicrosoftResolverService : IResolverService
    {
        public IServiceProvider Provider { get; set; }

        public T Resolve<T>()
        {
            return Provider.GetService<T>();
        }

        public object Resolve(Type t)
        {
            return Provider.GetService(t);
        }
    }
}
