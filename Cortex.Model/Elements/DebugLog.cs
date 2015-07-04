using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements
{
    [Serializable]
    [Export(typeof(IElement))]
    public class DebugLog : IElement
    {
        public string Name { get { return "Debug Log"; } }
        public string Category { get { return "Common"; } }
        public Uri IconUri { get { return new Uri("pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png"); } }
        public string Description { get { return "Logs to debug log"; } }
        public IInputPin[] Inputs { get; private set; }
        public IOutputPin[] Outputs { get; private set; }

        public DebugLog()
        {
            Inputs = new IInputPin[]
            {
                new FlowInputPin(OnCall),
                new InputPin("Object", typeof (object), null)
            };
            Outputs = new IOutputPin[]
            {
                new FlowOutputPin(),
            };
        }

        private void OnCall()
        {
            var flow = Inputs[0].Value as Flow;
            Debug.WriteLine(Inputs[1].Value);
            ((FlowOutputPin)Outputs[0]).Call(flow);
        }
    }
}