using System;
using System.Runtime.Serialization;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Elements;
using Cortex.Model.Pins;

namespace Cortex.Elements
{
    [Serializable]
    class LogElement : BaseElement
    {
        [NonSerialized]
        private ILog _log;

        public LogElement()
        {
            _log = LogManager.GetLog(this.GetType());
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
            var flow = Inputs[0].Value as Flow;
            var val = Inputs[1].Value;
            
            if (val != null && _log != null)
                _log.Info("Output: " + Inputs[1].Value);
            ((FlowOutputPin)Outputs[0]).Call(flow);
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            _log = LogManager.GetLog(this.GetType());
        }
    }
}
