using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public class RegistrantFinderBuilder
    {
        private readonly Dictionary<string, string> m_ConnectionStrings = new Dictionary<string, string>();
        private ITypesFinder m_TypesFinder;
        private readonly List<string> m_AssemblyPrefixes = new List<string>{"Com.Ericmas001."};

        public RegistrantFinderBuilder AddDatabase(string key, string connectionString)
        {
            m_ConnectionStrings.Add(key, connectionString);
            return this;
        }
        public RegistrantFinderBuilder AddAssemblyPrefix(string prefix)
        {
            m_AssemblyPrefixes.Add(prefix);
            return this;
        }
        public RegistrantFinderBuilder UsingTypeFinder(ITypesFinder tf)
        {
            m_TypesFinder = tf;
            return this;
        }

        public RegistrantFinder Build()
        {
            return new RegistrantFinder
            {
                ConnectionStrings = m_ConnectionStrings,
                TypesFinder = m_TypesFinder ?? new AssemblyTypeFinder(m_AssemblyPrefixes.ToArray())
            };
        }
    }
}
