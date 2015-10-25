using System.Runtime.Serialization;
using Caliburn.Micro;
using Cortex.Core.Elements;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Studio.Elements.LogElement
{
    class LogElement : BaseElement
    {
        private ILog _log;
        private readonly DataInputPin<object> _input = new DataInputPin<object>("Object");
        private readonly FlowOutputPin _output = new FlowOutputPin();

        public LogElement()
        {
            _log = LogManager.GetLog(this.GetType());
            AddInputPin(new FlowInputPin(Action));
            AddInputPin(_input);
            AddOutputPin(_output);
        }

        private void Action(Flow flow)
        {
            var val = _input.Value;

            if (val != null && _log != null)
                _log.Info("Output: " + val);

            _output.Call(flow);
        }

        void OnDeserialized(StreamingContext context)
        {
            _log = LogManager.GetLog(this.GetType());
        }
    }
}
