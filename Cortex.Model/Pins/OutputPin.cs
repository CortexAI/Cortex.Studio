using System;

namespace Cortex.Model.Pins
{
    [Serializable]
    public class OutputPin : IOutputPin
    {
        public string Name { get; protected set; }
        public Type Type { get; protected set; }
        public object Value { get; set; }

        public OutputPin(string name, Type type, object defaultValue = null)
        {
            Name = name;
            Type = type;
            Value = defaultValue;
        }
    }
}