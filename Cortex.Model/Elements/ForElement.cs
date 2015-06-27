using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Cortex.Model.Elements
{
    [Export(typeof(IElement))]
    public class ForElement : IElement
    {
        private int _index;
        public string Name { get { return "For Loop"; } }
        public string Category { get { return "Common"; } }
        public Uri IconUri { get { return null; } }
        public string Description { get { return "For Loop"; } }
        public IList<InputPin> Inputs { get; private set; }
        public IList<OutputPin> Outputs { get; private set; }

        public ForElement()
        {
            Inputs = new []
            {
                new FlowInputPin(OnCall),
                new InputPin("Times", typeof(int), 10)
            };

            Outputs = new[]
            {
                new FlowOutputPin("Each"),
                new OutputPin("Index", typeof (int), GetIndex),
                new FlowOutputPin("Finished"),
            };
        }

        private object GetIndex()
        {
            return _index;
        }

        private void OnCall()
        {
            for (_index = 0; _index < (int)Inputs[1].Value; _index++)
            {
                ((FlowOutputPin)Outputs[0]).Call();   
            }
            ((FlowOutputPin)Outputs[2]).Call();
        }
    }
}
