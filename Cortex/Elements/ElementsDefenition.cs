using System.ComponentModel.Composition;
using Cortex.Model;

namespace Cortex.Elements
{
    static class ElementsDefenition
    {
        [Export]
        public static ElementGroupDefenition EditorElements = new ElementGroupDefenition("Editor");

        [Export]
        public static ElementItemDefenition LogElement =
            new ElementItemDefenition<LogElement>(EditorElements, "Log to console", null, "Logs any data to console");
    }
}
