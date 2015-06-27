using System.ComponentModel.Composition;
using Cortex.Modules.ProcessDesigner.Commands;
using Gemini.Framework.Menus;

namespace Cortex.Modules.ProcessDesigner
{
    class MenuDefenitions
    {
        [Export]
        public static MenuItemDefinition NewProcessMenuDefenition = new CommandMenuItemDefinition<NewProcessCommandDefinition>(
            Gemini.Modules.MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 2);
    }
}
