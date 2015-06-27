using Gemini.Framework.Commands;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandDefinition]
    class OpenFolderDefenition : CommandDefinition
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
