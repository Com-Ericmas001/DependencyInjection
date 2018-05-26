using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ericmas001.DependencyInjection.Registrants.Interfaces
{
    public interface IConnectionStringRegistrant : IRegistrant
    {
        Dictionary<string,string> ConnectionStrings { get; set; }
    }
}
