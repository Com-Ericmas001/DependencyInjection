using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;

namespace Com.Ericmas001.DependencyInjection.Tests.Models
{
    public class DynamicRegistrantWithoutResolver : DynamicRegistrant
    {
        public DynamicRegistrantWithoutResolver(Action<DynamicRegistrant> registerFnct) : base(registerFnct)
        {
        }
        public new IEnumerable<IRegisteredElement> GetRegisteredTypeAssociation()
        {
            return base.GetRegisteredTypeAssociation().Where(x => x.RegisteredType != typeof(IResolverService));
        }
    }
}
