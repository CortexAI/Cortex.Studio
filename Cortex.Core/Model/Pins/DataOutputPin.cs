using System;

namespace Cortex.Core.Model.Pins
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

    public class DataOutputPin<T> : DataOutputPin
    {
        public DataOutputPin(string name) : base(name, typeof(T)) { }

        public new T Value
        {
            get { return (T)base.Value; }
            set { base.Value = value; }
        }
    }
}