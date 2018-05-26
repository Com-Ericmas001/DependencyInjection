using System;
using System.Collections.Generic;

namespace Com.Ericmas001.DependencyInjection.Exceptions
{
    public class CircularRegistrantDependencyException : Exception
    {
        public CircularRegistrantDependencyException(IEnumerable<Type> types) : base($"Some registrants have a circular dependency with each-other [{string.Join(", ", types)}]")
        {

        }
    }
}
