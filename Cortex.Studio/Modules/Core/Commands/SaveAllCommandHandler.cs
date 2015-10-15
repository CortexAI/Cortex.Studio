using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Gemini.Framework;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;

namespace Cortex.Studio.Modules.Core.Commands
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

        public async override Task Run(Command command)
        {
            foreach (var d in _shell.Documents.OfType<PersistedDocument>())
                await d.Save(d.FileName);
        }
    }
}
