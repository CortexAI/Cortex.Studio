using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Core.Elements.Logic
{
    public class Condition : BaseElement
    {
        private readonly DataInputPin<bool> _condition = new DataInputPin<bool>("Condition");
        private readonly FlowOutputPin _onTrue = new FlowOutputPin("True");
        private readonly FlowOutputPin _onFalse = new FlowOutputPin("False");

        public Condition()
        {
            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(_condition);
            AddOutputPin(_onTrue);
            AddOutputPin(_onFalse);
        }

        public void OnCall(Flow flow)
        {
            if (_condition.Value)
                _onTrue.Call(flow);
            else
                _onFalse.Call(flow);
        }
    }
}