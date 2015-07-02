using System.ComponentModel.Composition;
using Cortex.Modules.Core.Commands;
using Gemini.Framework.Menus;

namespace Cortex.Modules.Core
{
    class MenuDefenitions
    {
        [Export]
        public static MenuItemDefinition SaveMenuItem = new CommandMenuItemDefinition<SaveCommandDefenition>(
            Gemini.Modules.MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 3);

        [Export]
        public static MenuItemDefinition SaveAllMenuItem = new CommandMenuItemDefinition<SaveAllCommandDefenition>(
            Gemini.Modules.MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 4);
    }
}
