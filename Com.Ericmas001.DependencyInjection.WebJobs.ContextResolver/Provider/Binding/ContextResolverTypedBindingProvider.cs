using Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Exceptions;
using Microsoft.Azure.WebJobs.Host.Bindings;
using System;
using System.Collections.Concurrent;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Provider.Binding
{
    public class ContextResolverTypedBindingProvider
    {
        private readonly IContextResolver _resolver;
        private readonly ConcurrentDictionary<Type, IBinding> _typedBindings = new ConcurrentDictionary<Type, IBinding>();

        public ContextResolverTypedBindingProvider(IContextResolver resolver)
        {
            _resolver = resolver;
        }

        public IBinding ResolveBinding(Type parameterType)
        {
            return _typedBindings.GetOrAdd(parameterType, BuildContextResolverBinding);
        }

        private IBinding BuildContextResolverBinding(Type type)
        {
            Type genericType = typeof(ContextResolverBinding<>).MakeGenericType(type);

            return (IBinding)Activator.CreateInstance(genericType, new object[] { _resolver });
        }

        public static ContextResolverTypedBindingProvider FromContextResolverType(Type t)
        {
            if (!typeof(IContextResolver).IsAssignableFrom(t))
            {
                throw new ContextResolverTypeMismatchException(t.Name);
            }

            IContextResolver resolver = (IContextResolver)Activator.CreateInstance(t);

            return new ContextResolverTypedBindingProvider(resolver);
        }
    }
}
