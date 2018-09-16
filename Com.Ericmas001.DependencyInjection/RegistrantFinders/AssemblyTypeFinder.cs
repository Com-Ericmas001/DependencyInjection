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

        private readonly IEnumerable<Assembly> _extraAssemblies;
        private readonly string[] _prefixes;

        public AssemblyTypeFinder(IEnumerable<Assembly> extraAssemblies, params string[] prefixes)
        {
            _extraAssemblies = extraAssemblies;
            _prefixes = prefixes;
        }
        public IEnumerable<Type> FindTypesImplementing<TInterface>()
        {
            return GetAssemblies(Assembly.GetEntryAssembly())
                .Distinct(new AssemblyNameEqualityComparer())
                .Where(x => _extraAssemblies.All(y => x.Name != y.GetName().Name))
                .Select(Assembly.Load)
                .Concat(_extraAssemblies)
                .SelectMany(x => x.DefinedTypes)
                .Where(p => typeof(TInterface).IsAssignableFrom(p));
        }

        private IEnumerable<AssemblyName> GetAssemblies(Assembly a)
        {
            return a.GetReferencedAssemblies().Where(WantedPrefix).SelectMany(x => GetAssemblies(Assembly.Load(x))).Concat(new[] { a.GetName() });
        }

        private bool WantedPrefix(AssemblyName an)
        {
            foreach (var prefix in _prefixes)
                if (an.Name.StartsWith(prefix))
                    return true;
            return false;
        }
    }
}
