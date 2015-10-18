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
            new ElementItemDefenition<DisplayElement>(EditorElements, "Display", null, "Just to show");

        [Export]
        public static ElementEditorDefenition DisplayElementEditor =
            new ElementEditor<DisplayElement, DisplayElementViewModel, DisplayElementView>(true);

        [Export]
        public static ElementEditorDefenition LogElementEditor =
            new ElementEditor<LogElement, LogElementViewModel, LogElementView>();
    }
}
