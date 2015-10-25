using Cortex.Core.Elements;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;
using Microsoft.Kinect.Toolkit.FaceTracking;

namespace Cortex.Kinect
{
    class ActionUnits : BaseElement
    {
        private readonly DataInputPin _faceFramePin;
        private readonly FlowOutputPin _flowOut;
        private readonly DataOutputPin _au1;
        private readonly DataOutputPin _au2;
        private readonly DataOutputPin _au3;
        private readonly DataOutputPin _au4;
        private readonly DataOutputPin _au5;
        private readonly DataOutputPin _au6;

        public ActionUnits()
        {
            AddInputPin(new FlowInputPin(OnCall));

            _faceFramePin = new DataInputPin("Face Frame", typeof (FaceTrackFrame));
            AddInputPin(_faceFramePin);

            _flowOut = new FlowOutputPin();
            _au1 = new DataOutputPin("Brow Lower", typeof(double));
            _au2 = new DataOutputPin("Brow Raiser", typeof(double));
            _au3 = new DataOutputPin("Jaw Lower", typeof(double));
            _au4 = new DataOutputPin("Lip Corner Depressor", typeof(double));
            _au5 = new DataOutputPin("Lip Raiser", typeof(double));
            _au6 = new DataOutputPin("Lip Stretcher", typeof(double));

            AddOutputPin(_flowOut);
            AddOutputPin(_au1);
            AddOutputPin(_au2);
            AddOutputPin(_au3);
            AddOutputPin(_au4);
            AddOutputPin(_au5);
            AddOutputPin(_au6);
        }

        private void OnCall(Flow flow)
        {
            var frame = _faceFramePin.Value as FaceTrackFrame;
            if(frame == null)
                return;

            var aus = frame.GetAnimationUnitCoefficients();
            _au1.Value = aus[AnimationUnit.BrowLower];
            _au2.Value = aus[AnimationUnit.BrowRaiser];
            _au3.Value = aus[AnimationUnit.JawLower];
            _au4.Value = aus[AnimationUnit.LipCornerDepressor];
            _au5.Value = aus[AnimationUnit.LipRaiser];
            _au6.Value = aus[AnimationUnit.LipStretcher];
            _flowOut.Call(flow);
        }
    }
}
