using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Cortex.Model.Pins
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

        public void Call(Flow flow)
        {
            this.Value = flow;
            if(flow == null)
                throw  new Exception();

            try
            {
                Parallel.Invoke(new ParallelOptions { CancellationToken = flow.Token }, _actions.ToArray());
            }
            catch (OperationCanceledException)
            {
                // cancelled
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