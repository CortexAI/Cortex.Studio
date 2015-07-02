using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

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
        public InputPin[] Inputs { get; private set; }
        public OutputPin[] Outputs { get; private set; }

        public DebugLog()
        {
            Inputs = new []
            {
                new FlowInputPin(OnCall),
                new InputPin("Object", typeof (object), null)
            };
            Outputs = new OutputPin[]
            {
                new FlowOutputPin(),
            };
        }

        private void OnCall()
        {
            Debug.WriteLine(Inputs[1].Value);
            ((FlowOutputPin)Outputs[0]).Call();
        }
    }
}