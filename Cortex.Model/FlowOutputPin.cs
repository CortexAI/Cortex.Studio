using System;

namespace Cortex.Model
{
    [Serializable]
    public class FlowOutputPin : OutputPin
    {
        [NonSerialized]
        private Action _action;

        public FlowOutputPin(string name)
            : base(name, typeof(Flow))
        {

        }

        public FlowOutputPin()
            : base("Flow Out", typeof(Flow))
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