using System.Windows.Media.Imaging;
using Cortex.Core.Elements;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Studio.Elements.BitmapViewer
{
    class BitmapViewer : BaseElement
    {
        public WriteableBitmap Bitmap { get; private set; }

        private readonly DataInputPin<WriteableBitmap> _inputPin = new DataInputPin<WriteableBitmap>("Bitmap");
        private readonly FlowOutputPin _flowOutput = new FlowOutputPin();

        public BitmapViewer()
        {
            AddInputPin(new FlowInputPin(OnFlow));
            AddInputPin(_inputPin);
            AddOutputPin(_flowOutput);
        }

        private void OnFlow(Flow flow)
        {
            this.Bitmap = _inputPin.Value;
            _flowOutput.Call(flow);
        }
    }
}
