using System.ComponentModel.Composition;
using Cortex.Core.Model;

namespace Cortex.Core
{
    public class WellKnownGroups
    {
        [Export]
        public static ElementGroupDefenition Common = new ElementGroupDefenition("Common");
        
        [Export]
        public static ElementGroupDefenition MachineLearning = new ElementGroupDefenition("Machine Learning");

        [Export]
        public static ElementGroupDefenition Statistics = new ElementGroupDefenition("Statistics");

        [Export]
        public static ElementGroupDefenition Imaging = new ElementGroupDefenition("Imaging");

        [Export]
        public static ElementGroupDefenition SignalProcessing = new ElementGroupDefenition("Signal Processing");

        [Export]
        public static ElementGroupDefenition Plugins = new ElementGroupDefenition("Plugins");
    }
}
