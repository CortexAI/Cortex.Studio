using System.ComponentModel.Composition;
using Cortex.Studio.Modules.ProcessDesigner.Commands;
using Gemini.Framework.Menus;

namespace Cortex.Studio.Modules.ProcessDesigner
{
    class MenuDefenitions
    {
        [Export]
        public static MenuItemDefinition OpenOutputMenuItem = new CommandMenuItemDefinition<OpenStructureCommandDefenition>(
            Gemini.Modules.MainMenu.MenuDefinitions.ViewToolsMenuGroup, 3);
    }
}
