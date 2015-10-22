using System.ComponentModel.Composition;
using Cortex.Model;
using Cortex.Studio.Elements.BitmapViewer;

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
        public static ElementItemDefenition BitmapViewer =
            new ElementItemDefenition<BitmapViewer.BitmapViewer>(EditorElements, "Bitmap Viewer", null, "Shows bitmap viewer");

        [Export]
        public static ElementItemDefenition ToBitmap =
            new ElementItemDefenition<ToBitmap>(EditorElements, "To Bitmap", null, "Converts byte array to bitmap");

        [Export]
        public static ElementEditorDefenition DisplayElementEditor =
            new ElementEditor<DisplayElement, DisplayElementViewModel, DisplayElementView>(true);

        [Export]
        public static ElementEditorDefenition LogElementEditor =
            new ElementEditor<LogElement, LogElementViewModel, LogElementView>();

        [Export]
        public static ElementEditorDefenition BitmapViewerEditor =
            new ElementEditor<BitmapViewer.BitmapViewer, BitmapViewerViewModel, BitmapViewerView>(true);
    }
}
