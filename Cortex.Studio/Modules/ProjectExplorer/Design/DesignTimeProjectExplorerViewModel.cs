using System;
using Cortex.Studio.Modules.ProjectExplorer.ViewModels;

namespace Cortex.Studio.Modules.ProjectExplorer.Design
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
