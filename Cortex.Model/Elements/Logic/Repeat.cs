using System;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.Logic
{
    [Serializable]
    public class Repeat : BaseElement
    {
        private int _index;

        public Repeat()
        {
            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(new DataInputPin("Times", typeof(int)));

            AddOutputPin(new FlowOutputPin("Each"));
            AddOutputPin(new DataOutputPin("Index", typeof(int)));
            AddOutputPin(new FlowOutputPin("Finished"));
        }

        private void OnCall(Flow flow)
        {
            var count = GetInputData<int>(1);
            for (_index = 0; _index < count; _index++)
            {
                ((DataOutputPin)_outputs[1]).Value = _index;
                ((FlowOutputPin)_outputs[0]).Call(flow);   
            }
            ((FlowOutputPin)_outputs[2]).Call(flow);
        }
    }
}
