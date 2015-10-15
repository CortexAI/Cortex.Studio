using System;
using System.Threading;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.DebugElements
{
    [Serializable]
    class SleepElement : BaseElement
    {
        public SleepElement()
        {
            AddInputPin(new FlowInputPin(Action));
            AddInputPin(new DataInputPin("Time", typeof(int)));
            AddOutputPin(new FlowOutputPin("Out"));
        }

        private void Action(Flow flow)
        {
            Thread.Sleep(GetInputData<int>(1));
            ((FlowOutputPin)_outputs[0]).Call(flow);
        }
    }
}
