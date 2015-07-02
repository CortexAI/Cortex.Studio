using System;
using System.Windows.Input;
using Gemini.Framework.Commands;

namespace Cortex.Modules.Core.Commands
{
    [CommandDefinition]
    class SaveCommandDefenition : CommandDefinition
    {
        public const string CommandName = "File.Save";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Save"; }
        }

        public override string ToolTip
        {
            get { return "Save"; }
        }

        public override KeyGesture KeyGesture
        {
            get { return new KeyGesture(Key.S, ModifierKeys.Control); }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Resources/save_16xLG.png"); }
        }
    }
}
