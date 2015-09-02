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
        public static MenuItemDefinition OpenFolderMenuItem = new CommandMenuItemDefinition<OpenProjectDefenition>(
            Gemini.Modules.MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 1);

        [Export]
        public static MenuItemDefinition RecentMenuItem = new TextMenuItemDefinition(
            Gemini.Modules.MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 3, "_Recent");

        [Export]
        public static MenuItemGroupDefinition RecentCascadeGroup = new MenuItemGroupDefinition(RecentMenuItem, 0);

        [Export]
        public static MenuItemDefinition RecentMenuItemList = new CommandMenuItemDefinition<OpenExactProjectListDefenition>(RecentCascadeGroup, 0);
    }
}
