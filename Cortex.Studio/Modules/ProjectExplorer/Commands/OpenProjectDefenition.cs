using Gemini.Framework.Commands;

namespace Cortex.Studio.Modules.ProjectExplorer.Commands
{
    [CommandDefinition]
    class OpenProjectDefenition : CommandDefinition
    {
        public const string CommandName = "File.OpenFolder";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Open project"; }
        }

        public override string ToolTip
        {
            get { return "Opens project"; }
        }
    }
}
