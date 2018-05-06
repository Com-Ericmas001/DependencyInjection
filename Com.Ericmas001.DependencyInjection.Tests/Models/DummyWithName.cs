using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.DependencyInjection.Tests.Models
{
    public class DummyWithName : IDummy
    {
        public DummyWithName(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
