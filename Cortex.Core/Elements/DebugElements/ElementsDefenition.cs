using System.ComponentModel.Composition;
using Cortex.Core.Model;

namespace Cortex.Core.Elements.DebugElements
{
    internal class ElementsDefenition
    {
        [Export] 
        public static ElementGroupDefenition DebugElements = 
            new ElementGroupDefenition(WellKnownGroups.Common, "Debug");

        [Export] 
        public static ElementItemDefenition DebugLog =
            new ElementItemDefenition<DebugLog>(DebugElements, "Debug log", null, "Logs to visual studio debug output");

        [Export] 
        public static ElementItemDefenition SleepElement =
            new ElementItemDefenition<SleepElement>(DebugElements, "Sleep", null, "Thread sleep");
    }
}