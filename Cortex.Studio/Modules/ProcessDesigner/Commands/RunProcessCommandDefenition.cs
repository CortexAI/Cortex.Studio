using System;
using Gemini.Framework.Commands;

namespace Cortex.Studio.Modules.ProcessDesigner.Commands
{
    [CommandDefinition]
    class RunProcessCommandDefenition : CommandDefinition
    {
        public const string CommandName = "RunProcess";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Run"; }
        }

        public override string ToolTip
        {
            get { return "Run active process"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Resources/arrow_run_16xLG.png"); }
        }
    }
}
