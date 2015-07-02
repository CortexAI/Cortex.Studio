using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Cortex.Model
{
    [Serializable]
    public class FlowOutputPin : OutputPin
    {
        [NonSerialized]
        private List<Action> _actions = new List<Action>();

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
            if (_actions.Count <= 0) 
                return;
            _actions[0].Invoke();

            for (var i = 1; i < _actions.Count; i++)
            {
                var task = Task.Factory.StartNew(_actions[i]);
            }
        }

        public void Subscribe(Action action)
        {
            _actions.Add(action);
        }

        public void Unsubscribe(Action action)
        {
            if (_actions.Contains(action))
                _actions.Remove(action);
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            _actions = new List<Action>();
        }
    }
}