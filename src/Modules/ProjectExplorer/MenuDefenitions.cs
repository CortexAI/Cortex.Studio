using System.ComponentModel.Composition;
using Cortex.Modules.ProjectExplorer.Commands;
using Gemini.Framework.Menus;

namespace Cortex.Modules.ProjectExplorer
{
    class MenuDefenitions
    {
        [Export]
        public static MenuItemDefinition OpenProjectExplorerMenuItem = new CommandMenuItemDefinition<OpenProjectExplorerDefinition>(
            Gemini.Modules.MainMenu.MenuDefinitions.ViewToolsMenuGroup, 0);

        [Export]
        public static MenuItemDefinition OpenFolderMenuItem = new CommandMenuItemDefinition<OpenFolderDefenition>(
            Gemini.Modules.MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 1);
    }
}
