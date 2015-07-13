using System;
using Gemini.Framework.Commands;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandDefinition]
    class RefreshDefenition : CommandDefinition
    {
        public const string CommandName = "ProjectExplorer.Refresh";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Refresh"; }
        }

        public override string ToolTip
        {
            get { return "Refresh Project Explorer"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Resources/refresh_16xLG.png"); }
        }
    }
}
