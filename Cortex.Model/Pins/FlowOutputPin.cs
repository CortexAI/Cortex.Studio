using System;

namespace Cortex.Model.Pins
{
    public class FlowOutputPin : IFlowOutputPin
    {
        public string Name { get; private set; }

        public FlowOutputPin(string name)
        {
            Name = name;
        }

        public FlowOutputPin()
            : this("Flow out")
        {
        }

        public event Action<Flow> Called;
        public void Call(Flow flow)
        {
            var handler = Called;
            if(handler != null)
                handler.Invoke(flow);
        }
    }
}