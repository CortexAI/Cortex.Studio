using System;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.Logic
{
    [Serializable]
    public class Condition : BaseElement
    {
        public Condition()
        {
            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(new DataInputPin("Condition", typeof (bool)));
            AddOutputPin(new FlowOutputPin("True"));
            AddOutputPin(new FlowOutputPin("False"));
        }

        public void OnCall(Flow flow)
        {
            if (GetInputData<bool>(1))
                ((FlowOutputPin) _outputs[0]).Call(flow);
            else
                ((FlowOutputPin) _outputs[1]).Call(flow);
        }
    }
}