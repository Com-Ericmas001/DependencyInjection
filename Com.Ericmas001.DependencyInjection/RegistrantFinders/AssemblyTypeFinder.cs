using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public class AssemblyTypeFinder : ITypesFinder
    {
        public IEnumerable<Type> FindTypesImplementing<TInterface>()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(TInterface).IsAssignableFrom(p));
        }
    }
}
