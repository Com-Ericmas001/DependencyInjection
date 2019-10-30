using Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Attributes;
using Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Provider.Binding;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Provider.Config
{
    public class ContextResolverExtensionConfigProvider : IExtensionConfigProvider
    {
        private readonly ContextResolverBindingProdvider _bindingProdvider;

        public ContextResolverExtensionConfigProvider()
        {
            _bindingProdvider = new ContextResolverBindingProdvider();
        }

        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<ResolveAttribute>().Bind(_bindingProdvider);
        }
    }
}
