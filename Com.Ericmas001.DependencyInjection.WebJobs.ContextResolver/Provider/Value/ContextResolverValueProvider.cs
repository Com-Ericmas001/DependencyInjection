using Microsoft.Azure.WebJobs.Host.Bindings;
using System;
using System.Threading.Tasks;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Provider.Value
{
    public class ContextResolverValueProvider<T> : IValueProvider
    {
        private readonly IContextResolver _resolver;
        private readonly FunctionBindingContext _context;

        public ContextResolverValueProvider(IContextResolver resolver, FunctionBindingContext context)
        {
            _resolver = resolver;
            _context = context;
        }

        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public Task<object> GetValueAsync()
        {
            return Task.Run<object>(() => _resolver.Resolve<T>(_context));
        }

        public string ToInvokeString()
        {
            return null;
        }
    }
}
