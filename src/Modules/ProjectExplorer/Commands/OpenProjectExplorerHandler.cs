using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandHandler]
    class OpenProjectExplorerHandler : CommandHandlerBase<OpenProjectExplorerDefinition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public OpenProjectExplorerHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.ShowTool<ProjectExplorerViewModel>();
            return TaskUtility.Completed;
        }
    }
}
