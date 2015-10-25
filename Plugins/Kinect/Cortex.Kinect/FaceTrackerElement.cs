using System;
using Cortex.Core.Elements;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;
using Cortex.Kinect.Frames;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.FaceTracking;

namespace Cortex.Kinect
{
    class FaceTrackerElement : BaseElement, IDisposable
    {
        private FaceTracker _faceTracker;

        private readonly DataInputPin<ColorFrame> _colorImagePin = new DataInputPin<ColorFrame>("Color frame");
        private readonly DataInputPin<Skeleton> _skeletonPin = new DataInputPin<Skeleton>("Skeleton");
        private readonly DataInputPin<DepthFrame> _depthImagePin = new DataInputPin<DepthFrame>("Depth frame");
        private readonly DataInputPin<KinectSensor> _kinectPin = new DataInputPin<KinectSensor>("Kinect");

        private readonly FlowOutputPin _trackedPin = new FlowOutputPin("Successfull tracking");
        private readonly DataOutputPin<FaceTrackFrame> _faceTrackedFrame = new DataOutputPin<FaceTrackFrame>("Face frame");

        private SkeletonTrackingState _skeletonTrackingState;
        

        public FaceTrackerElement()
        {
            AddInputPin(new FlowInputPin(OnCall));

            AddInputPin(_kinectPin);
            AddInputPin(_colorImagePin);
            AddInputPin(_depthImagePin);
            AddInputPin(_skeletonPin);
            
            AddOutputPin(_trackedPin);
            AddOutputPin(_faceTrackedFrame);
        }

        private void OnCall(Flow flow)
        {
            var color = _colorImagePin.Value;
            var depth = _depthImagePin.Value;
            var skeleton = _skeletonPin.Value;
            var kinect = _kinectPin.Value;

            if(kinect == null || color == null || depth == null || skeleton == null)
                return;

            _skeletonTrackingState = skeleton.TrackingState;

            if (_skeletonTrackingState != SkeletonTrackingState.Tracked)
            {
                // nothing to do with an untracked skeleton.
                return;
            }

            if (_faceTracker == null)
            {
                try
                {
                    _faceTracker = new FaceTracker(kinect);
                }
                catch (InvalidOperationException)
                {
                    // During some shutdown scenarios the FaceTracker
                    // is unable to be instantiated.  Catch that exception
                    // and don't track a face.
                    _faceTracker = null;
                }
            }

            if (_faceTracker == null) 
                return;

            var frame = _faceTracker.Track(color.Format, color.Data, depth.Format, depth.Data, skeleton);
            
            if (frame.TrackSuccessful)
            {
                _faceTrackedFrame.Value = frame;
                _trackedPin.Call(flow);
            }
        }

        public void Dispose()
        {
            if (_faceTracker != null)
            {
                _faceTracker.Dispose();
                _faceTracker = null;
            }
        }
    }
}
