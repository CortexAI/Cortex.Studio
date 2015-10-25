using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Core.Elements.Logic
{
    public class StartPoint : BaseElement
    {
        private readonly FlowOutputPin _output;

        public StartPoint()
        {
            _output = new FlowOutputPin("Started");
            AddOutputPin(_output);
        }

        public void Run(Flow flow)
        {
            _output.Call(flow);
        }
    }
}