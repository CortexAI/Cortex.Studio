using System;
using Gemini.Framework.Commands;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandDefinition]
    class OpenExactProjectListDefenition : CommandListDefinition
    {
        public const string CommandName = "File.Recent";

        public override string Name
        {
            get { return CommandName; }
        }
    }
}
