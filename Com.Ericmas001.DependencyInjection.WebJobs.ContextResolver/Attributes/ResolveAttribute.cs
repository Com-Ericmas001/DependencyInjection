using System;
using Microsoft.Azure.WebJobs.Description;

namespace Com.Ericmas001.DependencyInjection.WebJobs.ContextResolver.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class ResolveAttribute : Attribute
    {
    }
}
