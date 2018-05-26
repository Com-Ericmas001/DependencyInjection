using System;

namespace Com.Ericmas001.DependencyInjection.Attributes
{
    public class MustRegisterAfterAttribute : Attribute
    {
        public Type[] PrerequisiteTypes { get; }
        public MustRegisterAfterAttribute(params Type[] prerequisiteTypeses)
        {
            PrerequisiteTypes = prerequisiteTypeses;
        }
    }
}
