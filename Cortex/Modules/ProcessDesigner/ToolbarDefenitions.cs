using System.ComponentModel.Composition;
using Cortex.Modules.ProcessDesigner.Commands;
using Gemini.Framework.ToolBars;

namespace Cortex.Modules.ProcessDesigner
{
    class ToolbarDefenitions
    {
        [Export]
        public static ToolBarDefinition ProcessToolBar = new ToolBarDefinition(1, "Process Designer");

        [Export]
        public static ToolBarItemGroupDefinition ProcessToolBarGroup = new ToolBarItemGroupDefinition(ProcessToolBar, 0);
        
        [Export]
        public static ToolBarItemDefinition RunToolBarItem = new CommandToolBarItemDefinition<RunProcessCommandDefenition>(
            ProcessToolBarGroup, 1, ToolBarItemDisplay.IconAndText);
        
        [Export]
        public static ToolBarItemDefinition StopToolBarItem = new CommandToolBarItemDefinition<StopProcessCommandDefenition>(
            ProcessToolBarGroup, 2, ToolBarItemDisplay.IconAndText);
    }
}
