using Cortex.Core.Elements;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;
using Microsoft.Kinect.Toolkit.FaceTracking;

namespace Cortex.Kinect
{
    class ActionUnits : BaseElement
    {
        private readonly DataInputPin<FaceTrackFrame> _faceFramePin = new DataInputPin<FaceTrackFrame>("Face Frame");
        private readonly FlowOutputPin _flowOut = new FlowOutputPin();
        private readonly DataOutputPin _au1 = new DataOutputPin<double>("Brow Lower");
        private readonly DataOutputPin _au2 = new DataOutputPin<double>("Brow Raiser");
        private readonly DataOutputPin _au3 = new DataOutputPin<double>("Jaw Lower");
        private readonly DataOutputPin _au4 = new DataOutputPin<double>("LipCorner Depressor");
        private readonly DataOutputPin _au5 = new DataOutputPin<double>("Lip Raiser");
        private readonly DataOutputPin _au6 = new DataOutputPin<double>("Lip Stretcher");

        public ActionUnits()
        {
            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(_faceFramePin);
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
            var frame = _faceFramePin.Value;
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
