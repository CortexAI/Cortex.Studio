using System.ComponentModel.Composition;
using Cortex.Core.Model;

namespace Cortex.Kinect
{
    internal class ElementsDefenition
    {
        [Export]
        public static ElementGroupDefenition KinectElements 
            = new ElementGroupDefenition("Kinect");

        [Export]
        public static ElementItemDefenition KinectSensor =
            new ElementItemDefenition<Kinect>(KinectElements, "Kinect sensor", null, "Microsoft Kinect API");

        [Export]
        public static ElementItemDefenition ToBitmap =
            new ElementItemDefenition<ToBytes>(KinectElements, "Frame to byte array", null, "Frame to bitmap converter");

        [Export]
        public static ElementItemDefenition SkeletonTracker =
            new ElementItemDefenition<SkeletonTracker>(KinectElements, "Skeleton tracker", null, "Tracks skeletons from Kinect skeleton stream");

        [Export]
        public static ElementItemDefenition FaceTracker =
            new ElementItemDefenition<FaceTrackerElement>(KinectElements, "Face tracker", null, "Microsoft Kinect Face Tracker");

        [Export]
        public static ElementItemDefenition ActionUnits =
            new ElementItemDefenition<ActionUnits>(KinectElements, "Action Units", null, "Retreives action units from face tracked frame");

    }
}
