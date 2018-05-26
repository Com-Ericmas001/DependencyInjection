using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.DependencyInjection.Attributes;
using Com.Ericmas001.DependencyInjection.Exceptions;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;

namespace Com.Ericmas001.DependencyInjection.RegistrantFinders
{
    public class RegistrantFinder
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
        public ITypesFinder TypesFinder { get; set; }

        public IEnumerable<IRegisteredElement> GetAllRegistrations()
        {
            List<IRegisteredElement> elements = new List<IRegisteredElement>();

            List<Type> all = TypesFinder.FindTypesImplementing<IRegistrant>().ToList();
            List<Type> done = new List<Type>();
            List<Type> waiting = new List<Type>();

            foreach (var t in all)
                Do(t, all, done, waiting, elements);

            var lastCount = int.MaxValue;
            while (waiting.Any() && waiting.Count < lastCount)
            {
                var todo = waiting.ToArray();
                lastCount = waiting.Count;
                waiting = new List<Type>();
                foreach (var t in todo)
                    Do(t, all, done, waiting, elements);
            }

            if(waiting.Any())
                throw new CircularRegistrantDependencyException(waiting);

            return elements;
        }

        private void Do(Type t, List<Type> all, List<Type> done, List<Type> waiting, List<IRegisteredElement> elements)
        {
            var ctor = t.GetConstructor(Type.EmptyTypes);
            if (ctor != null && t.GetCustomAttribute(typeof(ManualRegistrantAttribute)) == null)
            {
                var mustWaitAtt = (MustRegisterAfterAttribute) t.GetCustomAttribute(typeof(MustRegisterAfterAttribute));
                if (mustWaitAtt != null)
                {
                    if (mustWaitAtt.PrerequisiteTypes.Intersect(all).Except(done).Any())
                    {
                        waiting.Add(t);
                        return;
                    }
                }

                var registrant = (IRegistrant) ctor.Invoke(new object[0]);

                if (registrant is IConnectionStringRegistrant connectionStringRegistrant)
                    connectionStringRegistrant.ConnectionStrings = ConnectionStrings;

                elements.AddRange(registrant.GetRegisteredTypeAssociation());
            }

            done.Add(t);
        }
    }
}
