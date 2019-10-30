using System;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Exceptions
{
    public class ContextResolverNotFoundException : Exception
    {
        public ContextResolverNotFoundException(string parameterName)
            : base($"Couldn't find context resolver for {parameterName}")
        {
        }
    }
}
