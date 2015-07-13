using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandHandler]
    class AllFilesHandler : ICommandHandler<AllFilesDefenition>
    {
        private readonly ProjectExplorerViewModel _projectExplorer;

        [ImportingConstructor]
        public AllFilesHandler(ProjectExplorerViewModel projectExplorer)
        {
            _projectExplorer = projectExplorer;
        }

        public void Update(Command command)
        {
            command.Checked = _projectExplorer.ShowAllFiles;
        }

        public Task Run(Command command)
        {
            _projectExplorer.ShowAllFiles = !_projectExplorer.ShowAllFiles;
            return TaskUtility.Completed;
        }
    }
}
