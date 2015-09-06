using System;
using System.Threading;
using System.Threading.Tasks;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.Logic
{
    [Serializable]
    public class StartPoint : BaseElement
    {
        public StartPoint()
        {
            AddOutputPin(new FlowOutputPin("Started"));
        }

        public void Run()
        {
            var cts = new CancellationTokenSource();
            try
            {
                var pin = _outputs[0] as FlowOutputPin;
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