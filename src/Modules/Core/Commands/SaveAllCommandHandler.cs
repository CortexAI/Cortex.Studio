using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Modules.Core.Commands
{
    [CommandHandler]
    class SaveAllCommandHandler : CommandHandlerBase<SaveAllCommandDefenition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public SaveAllCommandHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            foreach (var d in _shell.Documents.OfType<FileDocument>())
                d.Save();

            return TaskUtility.Completed;
        }
    }
}
