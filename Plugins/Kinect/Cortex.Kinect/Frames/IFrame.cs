namespace Cortex.Kinect.Frames
{
    interface IFrame
    {
        int FrameNumber { get; }
        long TimeStamp { get; }
    }
}
