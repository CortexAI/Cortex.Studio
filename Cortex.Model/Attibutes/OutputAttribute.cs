using System;

namespace Cortex.Model.Attibutes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    class OutputAttribute : Attribute
    {
        public string Name { get; private set; }

        public OutputAttribute(string name)
        {
            Name = name;
        }
    }
}