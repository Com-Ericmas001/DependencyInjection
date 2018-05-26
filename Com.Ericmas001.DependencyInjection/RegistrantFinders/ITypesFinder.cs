using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public interface ITypesFinder
    {
        IEnumerable<Type> FindTypesImplementing<TInterface>();
    }
}
