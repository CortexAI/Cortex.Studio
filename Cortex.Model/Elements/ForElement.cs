using System;
using System.ComponentModel.Composition;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements
{
    [Serializable]
    [Export(typeof(IElement))]
    public class ForElement : IElement
    {
        private int _index;
        public string Name { get { return "For Loop"; } }
        public string Category { get { return "Common"; } }
        public Uri IconUri { get { return null; } }
        public string Description { get { return "For Loop"; } }
        public IInputPin[] Inputs { get; private set; }
        public IOutputPin[] Outputs { get; private set; }

        public ForElement()
        {
            Inputs = new IInputPin[]
            {
                new FlowInputPin(OnCall),
                new InputPin("Times", typeof(int), 10)
            };

            Outputs = new IOutputPin[]
            {
                new FlowOutputPin("Each"),
                new OutputPin("Index", typeof (int), 0),
                new FlowOutputPin("Finished"),
            };
        }

        private void OnCall()
        {
            var flow = Inputs[0].Value as Flow;
            var count = (int) Inputs[1].Value;
            for (_index = 0; _index < count; _index++)
            {
                Outputs[1].Value = _index;
                ((FlowOutputPin)Outputs[0]).Call(flow);   
            }
            ((FlowOutputPin)Outputs[2]).Call(flow);
        }
    }
}
