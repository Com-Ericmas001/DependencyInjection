using System.Collections.Generic;
using System.Reflection;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public class RegistrantFinderBuilder
    {
        private readonly Dictionary<string, string> _connectionStrings = new Dictionary<string, string>();
        private ITypesFinder _typesFinder;
        private readonly List<string> _assemblyPrefixes = new List<string> { "Com.Ericmas001." };
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public RegistrantFinderBuilder AddDatabase(string key, string connectionString)
        {
            _connectionStrings.Add(key, connectionString);
            return this;
        }
        public RegistrantFinderBuilder AddAssemblyPrefix(string prefix)
        {
            _assemblyPrefixes.Add(prefix);
            return this;
        }
        public RegistrantFinderBuilder AddAssembly(Assembly asmb)
        {
            _assemblies.Add(asmb);
            return this;
        }
        public RegistrantFinderBuilder UsingTypeFinder(ITypesFinder tf)
        {
            _typesFinder = tf;
            return this;
        }

        public RegistrantFinder Build()
        {
            return new RegistrantFinder
            {
                ConnectionStrings = _connectionStrings,
                TypesFinder = _typesFinder ?? new AssemblyTypeFinder(_assemblies, _assemblyPrefixes.ToArray())
            };
        }
    }
}
