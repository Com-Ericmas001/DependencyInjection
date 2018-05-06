using Com.Ericmas001.DependencyInjection.Registrants;

namespace Com.Ericmas001.DependencyInjection.Tests.Models
{
    public class BetterDummyRegistrant : AbstractRegistrant
    {
        protected override void RegisterEverything()
        {
            Register<BetterDummy>();
        }
    }
}
