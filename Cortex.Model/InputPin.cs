using System;

namespace Cortex.Model
{
    public class InputPin : IPin
    {
        public OutputPin ConnectedPin { get; protected set; }
        public string Name { get; protected set; }
        public Type Type { get; protected set; }

        public virtual Object Value
        {
            get
            {
                if(ConnectedPin != null)
                    return ConnectedPin.Value;
                return _selfValue;
            }
        }

        private readonly object _selfValue;

        public InputPin(string name, Type t, object defaultValue)
        {
            Name = name;
            Type = t;
            _selfValue = defaultValue;
        }

        public virtual void SetSourcePin(OutputPin pin)
        {
            if (pin == null)
            {
                ConnectedPin = null;
                return;
            }

            if(!Type.IsAssignableFrom(pin.Type))
                throw new Exception("Type mismatch");
            ConnectedPin = pin;
        }
    }

    public class FlowInputPin : InputPin
    {
        private readonly Action _action;

        public FlowInputPin(Action action) : base("Flow In", typeof(Flow), null)
        {
            _action = action;
        }

        public override void SetSourcePin(OutputPin pin)
        {
            base.SetSourcePin(pin);

            var flowOut = pin as FlowOutputPin;
            if (flowOut != null) flowOut.Subscribe(_action);
        }
    }
}