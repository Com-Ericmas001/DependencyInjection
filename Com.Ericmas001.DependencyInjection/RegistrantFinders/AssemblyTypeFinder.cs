using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public class AssemblyTypeFinder : ITypesFinder
    {
        private class AssemblyNameEqualityComparer : IEqualityComparer<AssemblyName>
        {
            public bool Equals(AssemblyName x, AssemblyName y)
            {
                return x.Name == y.Name;
            }

            public int GetHashCode(AssemblyName obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        private readonly string[] m_Prefixes;

        public AssemblyTypeFinder(params string[] prefixes)
        {
            m_Prefixes = prefixes;
        }
        public IEnumerable<Type> FindTypesImplementing<TInterface>()
        {
            return GetAssemblies(Assembly.GetEntryAssembly())
                .Distinct(new AssemblyNameEqualityComparer())
                .Select(x => Assembly.Load(x))
                .SelectMany(x => x.DefinedTypes)
                .Where(p => typeof(TInterface).IsAssignableFrom(p));
        }

        private IEnumerable<AssemblyName> GetAssemblies(Assembly a)
        {
            return a.GetReferencedAssemblies().Where(WantedPrefix).SelectMany(x => GetAssemblies(Assembly.Load(x))).Concat(new[] { a.GetName() });
        }

        private bool WantedPrefix(AssemblyName an)
        {
            foreach (var prefix in m_Prefixes)
                if (an.Name.StartsWith(prefix))
                    return true;
            return false;
        }
    }
}
