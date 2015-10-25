using System;
using System.Diagnostics;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Core.Elements.DebugElements
{
    public class DebugLog : BaseElement
    {
        private readonly DataInputPin<object> _input = new DataInputPin<object>("Object");
        private readonly FlowOutputPin _output = new FlowOutputPin();

        public DebugLog()
        {
            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(_input);
            AddOutputPin(_output);
        }

        private void OnCall(Flow flow)
        {
            Debug.WriteLine(_input.Value);
            Console.WriteLine(_input.Value);
            _output.Call(flow);
        }
    }
}