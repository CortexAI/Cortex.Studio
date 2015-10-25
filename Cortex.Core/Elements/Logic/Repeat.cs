using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Core.Elements.Logic
{
    public class Repeat : BaseElement
    {
        private readonly DataInputPin<int> _times = new DataInputPin<int>("Times");
        private readonly DataOutputPin<int> _index = new DataOutputPin<int>("Index");
        private readonly FlowOutputPin _each = new FlowOutputPin("Each");
        private readonly FlowOutputPin _finished = new FlowOutputPin("Finished");

        public Repeat()
        {
            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(_times);
            AddOutputPin(_each);
            AddOutputPin(_index);
            AddOutputPin(_finished);
        }

        private void OnCall(Flow flow)
        {
            var count = _times.Value;
            for (var i = 0; i < count; i++)
            {
                _index.Value = i;
                _each.Call(flow);
            }
            _finished.Call(flow);
        }
    }
}