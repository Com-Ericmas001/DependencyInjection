using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
{
    public abstract class AbstractRegistrant : IRegistrant
    {
        private readonly Dictionary<Type,Type> m_RegisteredTypeAssociation = new Dictionary<Type, Type>();

        protected abstract void RegisterEverything();

        public IEnumerable<KeyValuePair<Type, Type>> GetRegisteredTypeAssociation()
        {
            RegisterEverything();
            return m_RegisteredTypeAssociation.ToArray();
        }
        protected void Register<TInterface,TImplementation>() 
            where TImplementation : TInterface
        {
            m_RegisteredTypeAssociation[typeof(TInterface)] = typeof(TImplementation);
        }
    }
}
