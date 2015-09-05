using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Pins;

namespace Cortex.Elements
{
    [Serializable]
    class DisplayElement : PropertyChangedBase, IElement
    {
        public IEnumerable<IInputPin> Inputs { get { return _inputs; } }

        public IEnumerable<IOutputPin> Outputs { get { return _outputs; } }

        private string _value;
        private readonly IInputPin[] _inputs;
        private readonly IOutputPin[] _outputs;

        public string Value
        {
            get
            {
                return _value;
            }
            private set
            {
                _value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public DisplayElement()
        {
            _inputs = new IInputPin[]
            {
                new FlowInputPin(Action),
                new DataInputPin("Object",typeof(object)),
            };
            _outputs = new IOutputPin[]
            {
                new FlowOutputPin("Out")
            };
        }

        private void Action(Flow flow)
        {
            var o = ((DataInputPin) _inputs[1]).Value;
            Value = o != null ? o.ToString() : "null";
            ((FlowOutputPin)_outputs[0]).Call(flow);
        }
    }
}
