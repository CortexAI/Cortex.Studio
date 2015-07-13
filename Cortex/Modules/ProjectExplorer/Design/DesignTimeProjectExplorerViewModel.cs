using System;
using Cortex.Modules.ProjectExplorer.ViewModels;

namespace Cortex.Modules.ProjectExplorer.Design
{
    class DesignTimeProjectExplorerViewModel : ProjectExplorerViewModel
    {
        DesignTimeProjectExplorerViewModel()
            : base(null, null)
        {
            Root = new FolderItemViewModel(Environment.CurrentDirectory);
        }
    }
}
