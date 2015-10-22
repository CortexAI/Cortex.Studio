using System;

namespace Cortex.Model.Pins
{
    public class FlowOutputPin : IFlowOutputPin
    {
        public FlowOutputPin(string name)
        {
            Name = name;
        }

        public FlowOutputPin()
            : this("Out")
        {
        }

        public string Name { get; private set; }

        public event Action<Flow> Called;

        public void Call(Flow flow)
        {
            Action<Flow> handler = Called;
            if (handler != null)
                handler.Invoke(flow);
        }
    }
}