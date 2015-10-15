using System;

namespace Cortex.Model.Pins
{
    public class FlowInputPin : IFlowInputPin
    {
        private readonly Action<Flow> _handler;

        private FlowInputPin()
        {
        }

        public FlowInputPin(string name, Action<Flow> handler)
        {
            Name = name;
            _handler = handler;
        }

        public FlowInputPin(Action<Flow> handler)
            : this("Flow in", handler)
        {
        }

        public string Name { get; private set; }

        public void Attach(IOutputPin pin)
        {
            var outPin = pin as IFlowOutputPin;

            if (outPin == null)
                throw new Exception("Pin is not a flow pin");

            outPin.Called += _handler;
        }

        public void Detach(IOutputPin pin)
        {
            var outPin = pin as IFlowOutputPin;

            if (outPin == null)
                throw new Exception();

            outPin.Called -= _handler;
        }
    }
}