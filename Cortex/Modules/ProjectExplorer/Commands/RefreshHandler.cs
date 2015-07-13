using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandHandler]
    class RefreshHandler : CommandHandlerBase<RefreshDefenition>
    {
        private readonly ProjectExplorerViewModel _projectExplorer;

        [ImportingConstructor]
        public RefreshHandler(ProjectExplorerViewModel vm)
        {
            _projectExplorer = vm;
        }

        public override Task Run(Command command)
        {
            if (_projectExplorer != null)
                _projectExplorer.UpdateTree();
            return TaskUtility.Completed;
        }
    }
}
