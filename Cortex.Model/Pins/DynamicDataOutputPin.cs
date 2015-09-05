using System;

namespace Cortex.Model.Pins
{
    [Serializable]
    public class DynamicDataOutputPin : IDataOutputPin
    {
        private readonly Func<object> _func;
        public string Name { get; private set; }

        public object Value { get { return _func(); } }

        public Type Type { get; private set; }

        public DynamicDataOutputPin(string name, Type type, Func<object> func)
        {
            Name = name;
            Type = type;
            _func = func;
        }
    }
}