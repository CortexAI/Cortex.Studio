using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Cortex.Model.Elements
{
    [Export(typeof(IElement))]
    public class StartPoint : IElement
    {
        public string Name { get { return "Start Point"; } }
        public string Category { get { return "Common"; } }
        public Uri IconUri { get { return new Uri("pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png"); } }
        public string Description { get { return "Logs to debug log"; } }
        public IList<InputPin> Inputs { get; private set; }
        public IList<OutputPin> Outputs { get; private set; }

        public StartPoint()
        {
            Outputs = new OutputPin[]
            {
                new FlowOutputPin("Started")
            };
        }

        public void Run()
        {
            ((FlowOutputPin)Outputs[0]).Call();
        }
    }
}