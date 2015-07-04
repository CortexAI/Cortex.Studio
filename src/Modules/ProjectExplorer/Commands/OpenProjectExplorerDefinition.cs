using System;
using Gemini.Framework.Commands;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandDefinition]
    class OpenProjectExplorerDefinition : CommandDefinition
    {
        public const string CommandName = "View.ProjectExplorer";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Project Explorer"; }
        }

        public override string ToolTip
        {
            get { return "Opens Project Explorer"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Gemini;component/Resources/Icons/Open.png"); }
        }
    }
}
