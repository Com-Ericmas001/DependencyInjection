using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
{
    public abstract class AbstractRegistrant : IRegistrant
    {
        private readonly List<IRegisteredElement> m_RegisteredTypeAssociation = new List<IRegisteredElement>();

        protected abstract void RegisterEverything();

        public IEnumerable<IRegisteredElement> GetRegisteredTypeAssociation()
        {
            RegisterEverything();
            return m_RegisteredTypeAssociation.ToArray();
        }
        protected void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            m_RegisteredTypeAssociation.Add(new ImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation)));
        }
        protected void Register<TInterface, TImplementation>(string name)
            where TImplementation : TInterface
        {
            m_RegisteredTypeAssociation.Add(new NamedImplementationRegisteredElement(typeof(TInterface), typeof(TImplementation), name));
        }
    }
}
