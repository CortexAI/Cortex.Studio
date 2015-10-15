using Gemini.Framework.Commands;

namespace Cortex.Studio.Modules.Output.Commands
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
            get { return "Open output window"; }
        }
    }
}
