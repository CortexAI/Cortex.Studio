using System;
using System.Windows.Input;
using Gemini.Framework.Commands;

namespace Cortex.Modules.ProcessDesigner.Commands
{
    [CommandDefinition]
    public class NewProcessCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.New.NewProcess";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "New Process"; }
        }

        public override string ToolTip
        {
            get { return "Create new process"; }
        }

        public override KeyGesture KeyGesture
        {
            get { return new KeyGesture(Key.N, ModifierKeys.Control); }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Resources/action_create_16xLG.png"); }
        }
    }
}
