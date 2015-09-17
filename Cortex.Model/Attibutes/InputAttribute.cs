using System;

namespace Cortex.Model.Attibutes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    class InputAttribute : Attribute
    {
        public string Name { get; private set; }

        public InputAttribute(string name)
        {
            Name = name;
        }
    }
}