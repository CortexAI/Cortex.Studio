using System;
using System.Diagnostics;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.DebugElements
{
    public class DebugLog : BaseElement
    {
        public DebugLog()
        {
            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(new DataInputPin("Object", typeof (object)));

            AddOutputPin(new FlowOutputPin());
        }

        private void OnCall(Flow flow)
        {
            Debug.WriteLine(GetInputData<object>(1));
            Console.WriteLine(GetInputData<object>(1));
            ((FlowOutputPin) _outputs[0]).Call(flow);
        }
    }
}