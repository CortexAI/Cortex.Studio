using System.Collections.Generic;
using System.Linq;
using Cortex.Core.Elements;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;
using Microsoft.Kinect;

namespace Cortex.Kinect
{
    class SkeletonTracker : BaseElement
    {
        public class TrackedSkeleton
        {
            public Skeleton Skeleton { get; private set; }
            public int LastTrackedFrame { get; set; }

            public TrackedSkeleton(Skeleton skeleton, int frame)
            {
                Skeleton = skeleton;
                LastTrackedFrame = frame;
            }
        }

        private const uint MaxMissedFrames = 100;
        private readonly DataInputPin<Frames.SkeletonFrame> _skeletonFramePin = new DataInputPin<Frames.SkeletonFrame>("Skeleton frame");
        private readonly FlowOutputPin _skeletonTracked = new FlowOutputPin("Tracked");
        private readonly FlowOutputPin _skeletonUnTracked = new FlowOutputPin("Un Tracked");
        private readonly Dictionary<int, TrackedSkeleton> _trackedSkeletons = new Dictionary<int, TrackedSkeleton>();
        private readonly DataOutputPin<Skeleton> _lastTrackedPin = new DataOutputPin<Skeleton>("Last tracked skeleton");

        public SkeletonTracker()
        {
            AddInputPin(new FlowInputPin("On skeleton frame", OnSkeletonFrame));
            
            AddInputPin(_skeletonFramePin);
            AddOutputPin(_skeletonTracked);
            AddOutputPin(_skeletonUnTracked);
            AddOutputPin(_lastTrackedPin);
        }

        private void OnSkeletonFrame(Flow flow)
        {
            var skeletonFrame = _skeletonFramePin.Value;
            if(skeletonFrame == null)
                return;

            // Update the list of trackers and the trackers with the current frame information
            foreach (var skeleton in skeletonFrame.Skeletons)
            {
                if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    // We want keep a record of any skeleton, tracked or untracked.
                    if (!_trackedSkeletons.ContainsKey(skeleton.TrackingId))
                    {
                        _trackedSkeletons.Add(skeleton.TrackingId, new TrackedSkeleton(skeleton, skeletonFrame.FrameNumber));
                        _lastTrackedPin.Value = skeleton;
                        _skeletonTracked.Call(flow);
                    }

                    TrackedSkeleton skeletonRecord;
                    if (_trackedSkeletons.TryGetValue(skeleton.TrackingId, out skeletonRecord))
                    {
                        skeletonRecord.LastTrackedFrame = skeletonFrame.FrameNumber;
                    }
                }
            }

            var trackersToRemove = new List<int>();

            foreach (var tracker in _trackedSkeletons)
            {
                uint missedFrames = (uint)skeletonFrame.FrameNumber - (uint)tracker.Value.LastTrackedFrame;
                if (missedFrames > MaxMissedFrames)
                {
                    // There have been too many frames since we last saw this skeleton
                    trackersToRemove.Add(tracker.Key);
                }
            }

            foreach (var trackingId in trackersToRemove)
            {
                _trackedSkeletons.Remove(trackingId);
                if (_lastTrackedPin.Value != null && _lastTrackedPin.Value.TrackingId == trackingId)
                {
                    if (_trackedSkeletons.Any())
                        _lastTrackedPin.Value = _trackedSkeletons.First().Value.Skeleton;
                    else
                        _lastTrackedPin.Value = null;
                }
                _skeletonUnTracked.Call(flow);
            }
        }
    }
}
