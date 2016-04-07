using System.ComponentModel.Composition;
using Cortex.Studio.Modules.ProcessDesigner.Commands;
using Gemini.Framework.ToolBars;

namespace Cortex.Studio.Modules.ProcessDesigner
{
    class ToolbarDefenitions
    {
        [Export]
        public static ToolBarDefinition ProcessToolBar = new ToolBarDefinition(1, "ProcessGraph Designer");

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
