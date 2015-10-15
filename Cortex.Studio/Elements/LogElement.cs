using System.Runtime.Serialization;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Elements;
using Cortex.Model.Pins;

namespace Cortex.Studio.Elements
{
    class LogElement : BaseElement
    {
        private ILog _log;

        public LogElement()
        {
            _log = LogManager.GetLog(this.GetType());
            AddInputPin(new FlowInputPin(Action));
            AddInputPin(new DataInputPin("Object", typeof(object)));
            AddOutputPin(new FlowOutputPin());
        }

        private void Action(Flow flow)
        {
            var val = ((DataInputPin)_inputs[1]).Value;
            
            if (val != null && _log != null)
                _log.Info("Output: " + val);
            ((FlowOutputPin)_outputs[0]).Call(flow);
        }

        void OnDeserialized(StreamingContext context)
        {
            _log = LogManager.GetLog(this.GetType());
        }
    }
}
