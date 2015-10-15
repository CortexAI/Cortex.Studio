using System;
using Gemini.Framework.Commands;

namespace Cortex.Studio.Modules.ProcessDesigner.Commands
{
    [CommandDefinition]
    class StopProcessCommandDefenition : CommandDefinition
    {
        public const string CommandName = "StopProcess";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Stop"; }
        }

        public override string ToolTip
        {
            get { return "Stop active process"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Resources/Symbols_Stop_16xLG.png"); }
        }
    }
}
