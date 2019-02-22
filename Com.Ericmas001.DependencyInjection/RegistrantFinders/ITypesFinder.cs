using System;
using System.Collections.Generic;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public interface ITypesFinder
    {
        IEnumerable<Type> FindTypesImplementing<TInterface>();
    }
}
