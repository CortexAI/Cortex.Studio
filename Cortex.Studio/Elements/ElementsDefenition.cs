using System.ComponentModel.Composition;
using Cortex.Model;

namespace Cortex.Studio.Elements
{
    public static class EditorElementsDefenition
    {
        [Export]
        public static ElementGroupDefenition EditorElements = new ElementGroupDefenition("Editor");

        [Export]
        public static ElementItemDefenition LogElement =
            new ElementItemDefenition<LogElement>(EditorElements, "Log to console", null, "Logs any data to console");

        [Export]
        public static ElementItemDefenition UIElement =
            new ElementItemWithViewDefenition<DisplayElement,DisplayElementView>(EditorElements, "Display", null, "Just to show");
    }
}
