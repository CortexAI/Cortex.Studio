using System;

namespace Cortex.Model.Attibutes
{
    [AttributeUsage(AttributeTargets.Method)]
    class FlowInputAttribute : Attribute
    {
        public string Name { get; private set; }

        public FlowInputAttribute(string name)
        {
            Name = name;
        }
    }
}