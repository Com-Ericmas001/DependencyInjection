using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;

namespace Com.Ericmas001.DependencyInjection.Registrants.Interfaces
{
    public interface IRegistrant
    {
        IEnumerable<IRegisteredElement> GetRegisteredTypeAssociation();
    }
}
