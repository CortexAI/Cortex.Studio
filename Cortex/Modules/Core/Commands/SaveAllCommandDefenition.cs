using System;
using System.Windows.Input;
using Gemini.Framework.Commands;

namespace Cortex.Modules.Core.Commands
{
    [CommandDefinition]
    class SaveAllCommandDefenition : CommandDefinition
    {
        public const string CommandName = "File.SaveAll";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Save All"; }
        }

        public override string ToolTip
        {
            get { return "Save All"; }
        }

        public static CommandKeyboardShortcut KeyGesture =
           new CommandKeyboardShortcut<SaveAllCommandDefenition>(new KeyGesture(Key.S, ModifierKeys.Alt));

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Resources/Saveall_6518.png"); }
        }
    }
}
