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
