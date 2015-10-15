using System.ComponentModel.Composition;
using Cortex.Studio.Modules.ElementsToolbox.Commands;
using Gemini.Framework.Menus;

namespace Cortex.Studio.Modules.ElementsToolbox
{
    class MenuDefenitions
    {
        [Export]
        public static MenuItemDefinition OpenElementToolboxMenuItem = new CommandMenuItemDefinition<OpenElementsToolboxDefinition>(
            Gemini.Modules.MainMenu.MenuDefinitions.ViewToolsMenuGroup, 0);
    }
}
