using System;
using Gemini.Framework.Commands;

namespace Cortex.Studio.Modules.ProcessDesigner.Commands
{
    [CommandDefinition]
    class OpenEditorCommandDefenition : CommandDefinition
    {
        public const string CommandName = "Open Editor";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Open Editor"; }
        }

        public override string ToolTip
        {
            get { return "Opens editor for this element"; }
        }

        public override Uri IconSource
        {
            get { return null; }
        }
    }
}
