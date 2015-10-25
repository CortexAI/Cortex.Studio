using System;

namespace Cortex.Core.Model.Pins
{
    public class DynamicDataOutputPin : IDataOutputPin
    {
        private readonly Func<object> _func;

        public DynamicDataOutputPin(string name, Type type, Func<object> func)
        {
            Name = name;
            Type = type;
            _func = func;
        }

        public string Name { get; private set; }

        public object Value
        {
            get { return _func(); }
        }

        public Type Type { get; private set; }
    }

    public class DynamicDataOutputPin<T> : DynamicDataOutputPin
    {
        public DynamicDataOutputPin(string name, Func<object> func) : base(name, typeof(T), func) { }

        public new T Value
        {
            get { return (T) base.Value; }
        }
    }
}