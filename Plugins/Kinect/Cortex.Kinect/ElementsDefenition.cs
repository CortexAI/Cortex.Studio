using System.ComponentModel.Composition;
using Cortex.Model;

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
        public static ElementItemDefenition FaceTracker =
            new ElementItemDefenition<FaceTracker>(KinectElements, "Face tracker", null, "Microsoft Kinect Face Tracker");

    }
}
