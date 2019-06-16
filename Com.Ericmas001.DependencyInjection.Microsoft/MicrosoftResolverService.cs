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

        public T Resolve<T>(string name)
        {
            throw new NotImplementedException("https://github.com/aspnet/DependencyInjection/issues/473");
        }

        public object Resolve(Type t)
        {
            return Provider.GetService(t);
        }

        public object Resolve(Type t, string name)
        {
            throw new NotImplementedException("https://github.com/aspnet/DependencyInjection/issues/473");
        }
    }
}
