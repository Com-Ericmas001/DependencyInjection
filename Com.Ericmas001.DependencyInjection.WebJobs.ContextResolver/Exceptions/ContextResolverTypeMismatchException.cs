using System;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Exceptions
{
    public class ContextResolverTypeMismatchException : Exception
    {
        public ContextResolverTypeMismatchException(string typeName)
            : base($"Context resolver {typeName} was not of type {typeof(IContextResolver)}.")
        {
        }
    }
}
