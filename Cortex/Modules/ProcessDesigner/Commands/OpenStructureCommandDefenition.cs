using System;
using Gemini.Framework.Commands;

namespace Cortex.Modules.ProcessDesigner.Commands
{
    [CommandDefinition]
    class OpenStructureCommandDefenition : CommandDefinition
    {
        public const string CommandName = "ContainerStructure";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Container Structure"; }
        }

        public override string ToolTip
        {
            get { return "Opens active container structure view"; }
        }

        public override Uri IconSource
        {
            get { return null; }
        }
    }
}
