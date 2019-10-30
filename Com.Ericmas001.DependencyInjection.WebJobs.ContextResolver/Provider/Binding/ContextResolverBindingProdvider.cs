using Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Attributes;
using Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Exceptions;
using Microsoft.Azure.WebJobs.Host.Bindings;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Provider.Binding
{
    public class ContextResolverBindingProdvider : IBindingProvider
    {
        private ConcurrentDictionary<ParameterInfo, Type> _parameterResolverTypes = new ConcurrentDictionary<ParameterInfo, Type>();
        private ConcurrentDictionary<Type, ContextResolverTypedBindingProvider> _resolvers = new ConcurrentDictionary<Type, ContextResolverTypedBindingProvider>();

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            return Task.Run(() => TryCreate(context));
        }

        private IBinding TryCreate(BindingProviderContext context)
        {
            Type builder = _parameterResolverTypes.GetOrAdd(context.Parameter, GetContextResolverType);

            ContextResolverTypedBindingProvider genericContextResolver = _resolvers.GetOrAdd(builder, ContextResolverTypedBindingProvider.FromContextResolverType);

            return genericContextResolver.ResolveBinding(context.Parameter.ParameterType);
        }

        private Type GetContextResolverType(ParameterInfo parameter)
        {
            ContextResolverAttribute paramaterContainer = parameter.GetCustomAttribute<ContextResolverAttribute>();
            if (paramaterContainer?.Resolver != null)
            {
                return paramaterContainer.Resolver;
            }

            MethodInfo method = parameter.Member as MethodInfo;
            ContextResolverAttribute methodContainer = method?.GetCustomAttribute<ContextResolverAttribute>();
            if (methodContainer?.Resolver != null)
            {
                return methodContainer.Resolver;
            }

            ContextResolverAttribute classContainer = method?.DeclaringType?.GetCustomAttribute<ContextResolverAttribute>();
            if (classContainer?.Resolver != null)
            {
                return classContainer.Resolver;
            }

            throw new ContextResolverNotFoundException($"{method?.DeclaringType?.Name}.{method?.Name}.{parameter.Name}");
        }
    }
}
