using Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Provider.Value;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using System.Threading.Tasks;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Provider.Binding
{
    public class ContextResolverBinding<T> : IBinding
    {
        private readonly IContextResolver _contextResolver;
        public bool FromAttribute => true;

        public ContextResolverBinding(IContextResolver contextResolver)
        {
            _contextResolver = contextResolver;
        }

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            return Task.Run<IValueProvider>(() => new ContextResolverValueProvider<T>(_contextResolver, context.FunctionContext));
        }

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            return BindAsync(null, context.ValueContext);
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor();
        }
    }
}
