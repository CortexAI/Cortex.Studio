using System;

namespace Cortex.Model
{
    public class OutputPin : IPin
    {
        private readonly Func<object> _action;

        public string Name { get; protected set; }
        public Type Type { get; protected set; }

        public object Value
        {
            get
            {
                if(_action != null)
                    return _action.Invoke();
                return null;
            }
        }

        public OutputPin(string name, Type type, Func<object> action)
        {
            Name = name;
            Type = type;
            _action = action;
        }
    }

    public class FlowOutputPin : OutputPin
    {
        private Action _action;

        public FlowOutputPin(string name)
            : base(name, typeof(Flow), null)
        {

        }

        public FlowOutputPin()
            : base("Flow Out", typeof(Flow), null)
        {

        }

        public void Call()
        {
            if (_action != null)
                _action.Invoke();
        }

        public void Subscribe(Action action)
        {
            _action = action;
        }
    }
}