using System;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter)]
    public class ContextResolverAttribute : Attribute
    {
        public Type Resolver { get; }

        public ContextResolverAttribute(Type resolver)
        {
            Resolver = resolver;
        }
    }
}
