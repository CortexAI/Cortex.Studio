using System;

namespace Cortex.Model.Pins
{
    public class DataOutputPin : IDataOutputPin
    {
        private DataOutputPin()
        {
        }

        public DataOutputPin(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }

        public object Value { get; set; }

        public Type Type { get; private set; }
    }
}