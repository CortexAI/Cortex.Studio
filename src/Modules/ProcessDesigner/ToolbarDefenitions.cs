using System.ComponentModel.Composition;
using Cortex.Modules.ProcessDesigner.Commands;
using Gemini.Framework.ToolBars;

namespace Cortex.Modules.ProcessDesigner
{
    class ToolbarDefenitions
    {
        [Export]
        public static ToolBarDefinition ProcessToolBar = new ToolBarDefinition(0, "Process Designer");

        [Export]
        public static ToolBarItemGroupDefinition ProcessToolBarGroup = new ToolBarItemGroupDefinition(ProcessToolBar, 0);

        [Export]
        public static ToolBarItemDefinition NewGraphToolBarItem = new CommandToolBarItemDefinition<NewProcessCommandDefinition>(
            ProcessToolBarGroup, 0, ToolBarItemDisplay.IconAndText);

        [Export]
        public static ToolBarItemDefinition RunToolBarItem = new CommandToolBarItemDefinition<RunProcessCommandDefenition>(
            ProcessToolBarGroup, 1, ToolBarItemDisplay.IconAndText);
    }
}
