using System.Collections.Generic;

namespace Com.Ericmas001.DependencyInjection.Registrants.Interfaces
{
    public interface IConnectionStringRegistrant : IRegistrant
    {
        Dictionary<string,string> ConnectionStrings { get; set; }
    }
}
