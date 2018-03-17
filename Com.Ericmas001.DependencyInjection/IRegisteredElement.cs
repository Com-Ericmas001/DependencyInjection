using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.DependencyInjection
{
    public interface IRegisteredElement
    {
        Type RegisteredType { get; }
    }
}
