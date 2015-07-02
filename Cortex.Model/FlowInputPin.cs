using System;

namespace Cortex.Model
{
    [Serializable]
    public class FlowInputPin : InputPin
    {
        private readonly Action _action;
        
        public FlowInputPin(Action action) : base("Flow In", typeof(Flow), null)
        {
            _action = action;
        }

        public override void SetSourcePin(OutputPin pin)
        {
            var source = ConnectedPin as FlowOutputPin;
            if (source != null)
                source.Unsubscribe(_action);

            base.SetSourcePin(pin);

            var flowOut = pin as FlowOutputPin;
            if (flowOut != null) flowOut.Subscribe(_action);
        }
    }
}