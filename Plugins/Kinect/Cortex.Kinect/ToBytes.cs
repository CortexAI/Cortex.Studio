using Cortex.Core.Elements;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;
using Cortex.Kinect.Frames;

namespace Cortex.Kinect
{
    class ToBytes : BaseElement
    {
        private readonly FlowOutputPin _flowOut;
        private readonly DataInputPin<IBytesConvertable> _framePin = new DataInputPin<IBytesConvertable>("Frame");
        private readonly DataOutputPin _dataPin = new DataOutputPin<byte[]>("Data");

        public ToBytes()
        {
            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(_framePin);

            _flowOut = new FlowOutputPin();
            AddOutputPin(_flowOut);
            AddOutputPin(_dataPin);
        }

        private void OnCall(Flow flow)
        {
            _dataPin.Value = _framePin.Value.ToBytes();
            _flowOut.Call(flow);
        }
    }
}
