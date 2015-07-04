using System;
using System.ComponentModel.Composition;
using System.Threading;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements
{
    [Export(typeof(IElement))]
    [Serializable]
    class SleepElement : BaseElement
    {
        public override string Description
        {
            get { return "Thread sleep"; }
        }

        public override string Category
        {
            get { return "Common"; }
        }

        public override string Name
        {
            get { return "Sleep"; }
        }

        public override Uri IconUri
        {
            get { return null; }
        }

        public SleepElement()
        {
            Inputs = new IInputPin[]
            {
                new FlowInputPin(Action),
                new InputPin("Time", typeof(int), 1000), 
            };

            Outputs = new IOutputPin[]
            {
                new FlowOutputPin("Out"),
            };
        }

        private void Action()
        {
            var flow = Inputs[0].Value as Flow;
            Thread.Sleep((int)Inputs[1].Value);
            ((FlowOutputPin)Outputs[0]).Call(flow);
        }
    }
}
