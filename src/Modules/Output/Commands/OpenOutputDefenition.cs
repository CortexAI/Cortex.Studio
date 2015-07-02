using Gemini.Framework.Commands;

namespace Cortex.Modules.Output.Commands
{
    [CommandDefinition]
    class OpenOutputDefenition : CommandDefinition
    {
        public const string CommandName = "View.Output";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Output"; }
        }

        public override string ToolTip
        {
            get { return "OpenProject output window"; }
        }
    }
}
