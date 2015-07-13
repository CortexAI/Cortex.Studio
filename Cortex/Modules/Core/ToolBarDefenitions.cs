using System.ComponentModel.Composition;
using Cortex.Modules.Core.Commands;
using Gemini.Framework.ToolBars;
using Gemini.Modules.Shell;

namespace Cortex.Modules.Core
{
    class ToolBarDefenitions
    {
        [Export]
        public static ToolBarItemDefinition Save = new CommandToolBarItemDefinition<SaveCommandDefenition>(ToolBarDefinitions.StandardOpenSaveToolBarGroup, 1);

        [Export]
        public static ToolBarItemDefinition SaveAll = new CommandToolBarItemDefinition<SaveAllCommandDefenition>(ToolBarDefinitions.StandardOpenSaveToolBarGroup, 2);
    }
}
