﻿using Cortex.Kinect.Frames;
using Cortex.Model;
using Cortex.Model.Elements;
using Cortex.Model.Pins;

namespace Cortex.Kinect
{
    class ToBytes : BaseElement
    {
        private readonly FlowOutputPin _flowOut;
        private readonly DataInputPin _framePin;
        private readonly DataOutputPin _dataPin;

        public ToBytes()
        {
            AddInputPin(new FlowInputPin(OnCall));
            _framePin = new DataInputPin("Frame", typeof(IBytesConvertable));
            AddInputPin(_framePin);

            _flowOut = new FlowOutputPin();
            _dataPin = new DataOutputPin("Data", typeof (byte[]));
            AddOutputPin(_flowOut);
            AddOutputPin(_dataPin);
        }

        private void OnCall(Flow flow)
        {
            _dataPin.Value = ((IBytesConvertable)_framePin.Value).ToBytes();
            _flowOut.Call(flow);
        }
    }
}
