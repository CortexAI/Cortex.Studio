using System.ComponentModel.Composition;
using Cortex.Modules.Output.Commands;
using Gemini.Framework.Menus;

namespace Cortex.Modules.Output
{
    class MenuDefenitions
    {
        [Export]
        public static MenuItemDefinition OpenOutputMenuItem = new CommandMenuItemDefinition<OpenOutputDefenition>(
            Gemini.Modules.MainMenu.MenuDefinitions.ViewToolsMenuGroup, 3);
    }
}
