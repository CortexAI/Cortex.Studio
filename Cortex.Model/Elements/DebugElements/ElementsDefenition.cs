using System.ComponentModel.Composition;

namespace Cortex.Model.Elements.DebugElements
{
    class ElementsDefenition
    {
        [Export]
        public static ElementGroupDefenition DebugElements = new ElementGroupDefenition("Debug");

        [Export]
        public static ElementGroupDefenition NestedElements = new ElementGroupDefenition(DebugElements, "Nested");

        [Export]
        public static ElementItemDefenition DebugLog =
            new ElementItemDefenition<DebugLog>(DebugElements, "Debug log", null, "Logs to visual studio debug output");

        [Export]
        public static ElementItemDefenition SleepElement =
            new ElementItemDefenition<SleepElement>(DebugElements, "Sleep", null, "Thread sleep");

        [Export]
        public static ElementItemDefenition DebugLog2 =
            new ElementItemDefenition<DebugLog>(NestedElements, "Debug log", null, "Logs to visual studio debug output");

        [Export]
        public static ElementItemDefenition SleepElement2 =
            new ElementItemDefenition<SleepElement>(NestedElements, "Sleep", null, "Thread sleep");
    }
}
