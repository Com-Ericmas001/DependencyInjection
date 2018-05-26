using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public class AssemblyTypeFinder : ITypesFinder
    {
        public IEnumerable<Type> FindTypesImplementing<TInterface>()
        {
            return Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(p => typeof(TInterface).IsAssignableFrom(p));
        }
    }
}
