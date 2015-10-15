using System.ComponentModel.Composition;
using Cortex.Studio.Modules.Core.Commands;
using Gemini.Framework.ToolBars;
using Gemini.Modules.Shell;

namespace Cortex.Studio.Modules.Core
{
    class ToolBarDefenitions
    {
        [Export]
        public static ToolBarItemDefinition SaveAll = new CommandToolBarItemDefinition<SaveAllCommandDefenition>(ToolBarDefinitions.StandardOpenSaveToolBarGroup, 3);
    }
}
