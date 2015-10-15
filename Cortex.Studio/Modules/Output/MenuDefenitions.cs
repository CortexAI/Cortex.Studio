using System.ComponentModel.Composition;
using Cortex.Studio.Modules.Output.Commands;
using Gemini.Framework.Menus;

namespace Cortex.Studio.Modules.Output
{
    class MenuDefenitions
    {
        [Export]
        public static MenuItemDefinition OpenOutputMenuItem = new CommandMenuItemDefinition<OpenOutputDefenition>(
            Gemini.Modules.MainMenu.MenuDefinitions.ViewToolsMenuGroup, 3);
    }
}
