using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
{
    public interface IRegistrant
    {
        IEnumerable<KeyValuePair<Type, Type>> GetRegisteredTypeAssociation();
    }
}
