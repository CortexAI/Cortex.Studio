using System;
using System.ComponentModel.Composition;
using System.Threading;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.Logic
{
    [Serializable]
    public class StartPoint : IElement
    {
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
            var cts = new CancellationTokenSource();
            try
            {
                var pin = Outputs[0] as FlowOutputPin;
                if (pin == null)
                    throw new NullReferenceException();
                var flowObject = new Flow(cts.Token);
                pin.Call(flowObject);
            }
            catch (ThreadInterruptedException)
            {
                cts.Cancel();
            }
        }
    }
}