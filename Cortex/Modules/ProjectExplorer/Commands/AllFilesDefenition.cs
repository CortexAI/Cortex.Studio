using System;
using Gemini.Framework.Commands;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandDefinition]
    class AllFilesDefenition : CommandDefinition
    {
        public const string CommandName = "ProjectExplorer.AllFiles";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "All Files"; }
        }

        public override string ToolTip
        {
            get { return "Show all Files"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Resources/ShowAllFiles_349.png"); }
        }
    }
}
