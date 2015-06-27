using Gemini.Framework.Commands;

namespace Cortex.Modules.ElementsToolbox.Commands
{
    [CommandDefinition]
    public class OpenElementsToolboxDefinition : CommandDefinition
    {
        public const string CommandName = "View.ElementsToolbox";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Elements Toolbox"; }
        }

        public override string ToolTip
        {
            get { return "Open Elements Toolbox"; }
        }
    }
}
