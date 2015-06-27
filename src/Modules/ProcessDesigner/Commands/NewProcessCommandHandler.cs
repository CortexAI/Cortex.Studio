using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Modules.ProcessDesigner.Commands
{
    [CommandHandler]
    public class NewProcessCommandHandler : CommandHandlerBase<NewProcessCommandDefinition>
    {
        private readonly IShell _shell;
        private readonly EditorProvider _editor;

        [ImportingConstructor]
        public NewProcessCommandHandler(IShell shell, EditorProvider editor)
        {
            _shell = shell;
            _editor = editor;
        }

        public override Task Run(Command command)
        {
            _shell.OpenDocument(_editor.CreateNew("Untitled"));
            return TaskUtility.Completed;
        }
    }
}
