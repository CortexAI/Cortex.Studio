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
        
        private readonly DataInputPin _colorImagePin;
        private readonly DataInputPin _skeletonPin;
        private readonly DataInputPin _depthImagePin;
        private readonly DataInputPin _kinectPin;

        private readonly FlowOutputPin _trackedPin;
        private readonly DataOutputPin _faceTrackedFrame;

        private SkeletonTrackingState _skeletonTrackingState;
        

        public FaceTrackerElement()
        {
            AddInputPin(new FlowInputPin(OnCall));

            _kinectPin = new DataInputPin("Kinect", typeof (KinectSensor));
            _colorImagePin = new DataInputPin("Color frame", typeof (ColorFrame));
            _depthImagePin = new DataInputPin("Depth frame", typeof (DepthFrame));
            _skeletonPin = new DataInputPin("Skeleton", typeof (Skeleton));

            AddInputPin(_kinectPin);
            AddInputPin(_colorImagePin);
            AddInputPin(_depthImagePin);
            AddInputPin(_skeletonPin);

            _trackedPin = new FlowOutputPin("Successfull tracking");
            _faceTrackedFrame = new DataOutputPin("Face frame", typeof (FaceTrackFrame));
            AddOutputPin(_trackedPin);
            AddOutputPin(_faceTrackedFrame);
        }

        private void OnCall(Flow flow)
        {
            var color = _colorImagePin.Value as ColorFrame;
            var depth = _depthImagePin.Value as DepthFrame;
            var skeleton = _skeletonPin.Value as Skeleton;
            var kinect = _kinectPin.Value as KinectSensor;

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
