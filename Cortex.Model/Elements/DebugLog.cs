using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace Cortex.Model.Elements
{
    [Export(typeof(IElement))]
    public class DebugLog : IElement
    {
        public string Name { get { return "Debug Log"; } }
        public string Category { get { return "Common"; } }
        public Uri IconUri { get { return new Uri("pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png"); } }
        public string Description { get { return "Logs to debug log"; } }
        public IList<InputPin> Inputs { get; private set; }
        public IList<OutputPin> Outputs { get; private set; }

        public DebugLog()
        {
            Inputs = new List<InputPin>
            {
                new FlowInputPin(OnCall),
                new InputPin("Object", typeof (object), null)
            };
            Outputs = new List<OutputPin>
            {
                new FlowOutputPin(),
            };
        }

        private void OnCall()
        {
            Debug.WriteLine(Inputs[1].Value);
        }
    }
}