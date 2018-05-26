using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public class RegistrantFinderBuilder
    {
        private readonly Dictionary<string, string> m_ConnectionStrings = new Dictionary<string, string>();
        private ITypesFinder TypesFinder = new AssemblyTypeFinder();

        public RegistrantFinderBuilder AddDatabase(string key, string connectionString)
        {
            m_ConnectionStrings.Add(key, connectionString);
            return this;
        }
        public RegistrantFinderBuilder UsingTypeFinder(ITypesFinder tf)
        {
            TypesFinder = tf;
            return this;
        }

        public RegistrantFinder Build()
        {
            return new RegistrantFinder
            {
                ConnectionStrings = m_ConnectionStrings,
                TypesFinder = TypesFinder
            };
        }
    }
}
