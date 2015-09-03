using System;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Pins;

namespace Cortex.Elements
{
    [Serializable]
    class DisplayElement : PropertyChangedBase, IElement
    {
        public IInputPin[] Inputs { get; private set; }

        public IOutputPin[] Outputs { get; private set; }

        private string _value;

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
            Inputs = new IInputPin[]
            {
                new FlowInputPin(Action),
                new InputPin("Object", typeof(object), null),
            };
            Outputs = new IOutputPin[]
            {
                new FlowOutputPin("Out")
            };
        }

        private void Action()
        {
            Value = Inputs[1].Value.ToString();
        }
    }
}
