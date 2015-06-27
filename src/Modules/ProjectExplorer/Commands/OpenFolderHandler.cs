using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cortex.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using Microsoft.Win32;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandHandler]
    class OpenFolderHandler : CommandHandlerBase<OpenFolderDefenition>
    {
        private ProjectExplorerViewModel _vm;

        [ImportingConstructor]
        public OpenFolderHandler(ProjectExplorerViewModel vm)
        {
            _vm = vm;
        }

        public override Task Run(Command command)
        {
            var dialog = new OpenFileDialog();
            var showDialog = dialog.ShowDialog();
            if (showDialog != null && (bool) showDialog)
            {
                _vm.Open(dialog.FileName);
            }
            return TaskUtility.Completed;
        }
    }
}
