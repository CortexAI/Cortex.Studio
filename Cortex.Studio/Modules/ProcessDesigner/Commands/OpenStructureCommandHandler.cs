using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Studio.Modules.ProcessDesigner.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Studio.Modules.ProcessDesigner.Commands
{
    [CommandHandler]
    class OpenStructureCommandHandler : CommandHandlerBase<OpenStructureCommandDefenition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public OpenStructureCommandHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.ShowTool<StructureViewModel>();
            return TaskUtility.Completed;
        }
    }
}
