using System.Threading;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Core.Elements.DebugElements
{
    internal class SleepElement : BaseElement
    {
        private readonly DataInputPin<int> _time = new DataInputPin<int>("Time");
        private readonly FlowOutputPin _output = new FlowOutputPin();

        public SleepElement()
        {
            AddInputPin(new FlowInputPin(Action));
            AddInputPin(_time);
            AddOutputPin(_output);
        }

        private void Action(Flow flow)
        {
            Thread.Sleep(_time.Value);
            _output.Call(flow);
        }
    }
}