using System.ComponentModel.Composition;
using Cortex.Modules.ElementsToolbox.Commands;
using Gemini.Framework.Menus;

namespace Cortex.Modules.ElementsToolbox
{
    class MenuDefenitions
    {
        [Export]
        public static MenuItemDefinition OpenElementToolboxMenuItem = new CommandMenuItemDefinition<OpenElementsToolboxDefinition>(
            Gemini.Modules.MainMenu.MenuDefinitions.ViewToolsMenuGroup, 0);
    }
}
