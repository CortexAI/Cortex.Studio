using System.ComponentModel.Composition;
using Cortex.Core;
using Cortex.Core.Model;
using Cortex.Studio.Elements.BitmapViewer;
using Cortex.Studio.Elements.Display;
using Cortex.Studio.Elements.LogElement;


namespace Cortex.Studio.Elements
{
    public static class EditorElementsDefenition
    {
        [Export]
        public static NodeGroupDefenition EditorElements = 
            new NodeGroupDefenition(WellKnownGroups.Common, "Editor");


        [Export]
        public static NodeDefenition ToBitmap =
          new NodeDefenition<ToBitmap>(EditorElements, "To Bitmap", null, "Converts byte array to bitmap");



        [Export]
        public static NodeDefenition LogElement =
            new NodeDefenition<LogElement.LogElement>(EditorElements, "Log to console", null, "Logs any data to console");

        [Export]
        public static NodeDefenition UINode = 
            new NodeDefenition<DisplayElement>(EditorElements, "Display", null, "Just to show");

        [Export]
        public static NodeDefenition BitmapViewer =
            new NodeDefenition<BitmapViewer.BitmapViewer>(EditorElements, "Bitmap Viewer", null, "Shows bitmap viewer");



        [Export]
        public static ElementEditorDefenition LogElementEditor =
         new ElementEditor<LogElement.LogElement, LogElementViewModel, LogElementView>();
        
        [Export]
        public static ElementEditorDefenition DisplayElementEditor =
            new ElementEditor<DisplayElement, DisplayElementViewModel, DisplayElementView>(true);
        
        [Export]
        public static ElementEditorDefenition BitmapViewerEditor =
            new ElementEditor<BitmapViewer.BitmapViewer, BitmapViewerViewModel, BitmapViewerView>(true);
    }
}
