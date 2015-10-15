using System.ComponentModel.Composition;
using Cortex.Studio.Modules.ProjectExplorer.Commands;
using Gemini.Framework.ToolBars;

namespace Cortex.Studio.Modules.ProjectExplorer
{
    class ToolBarDefenitions
    {
        public static ToolBarDefinition ProjectExplorerToolBar = new ToolBarDefinition(0, "Project Explorer");

        [Export]
        public static ToolBarItemGroupDefinition ProjectExplorerGroup = new ToolBarItemGroupDefinition(ProjectExplorerToolBar, 0);

        [Export]
        public static ToolBarItemDefinition RefreshToolBarItem =
            new CommandToolBarItemDefinition<RefreshDefenition>(ProjectExplorerGroup, 0, ToolBarItemDisplay.IconAndText);

        [Export]
        public static ToolBarItemDefinition StopToolBarItem =
            new CommandToolBarItemDefinition<AllFilesDefenition>(ProjectExplorerGroup, 1, ToolBarItemDisplay.IconAndText);
    }
}
