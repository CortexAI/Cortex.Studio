using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements
{
    [Export(typeof(IElement))]
    [Serializable]
    public class StartPoint : IElement
    {
        public string Name { get { return "Start Point"; } }
        public string Category { get { return "Common"; } }
        public Uri IconUri { get { return new Uri("pack://application:,,,/Modules/ProcessDesigner/Resources/color_swatch.png"); } }
        public string Description { get { return "Logs to debug log"; } }
        public IInputPin[] Inputs { get; private set; }
        public IOutputPin[] Outputs { get; private set; }

        public StartPoint()
        {
            Outputs = new IOutputPin[]
            {
                new FlowOutputPin("Started")
            };
        }

        public void Run()
        {
            var pin = Outputs[0] as FlowOutputPin;
            if (pin == null)
                throw new NullReferenceException();
            var task = Task.Factory.StartNew(pin.Call);
        }
    }
}