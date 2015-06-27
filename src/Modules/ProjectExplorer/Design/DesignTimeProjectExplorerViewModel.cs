using Cortex.Modules.ProjectExplorer.ViewModels;

namespace Cortex.Modules.ProjectExplorer.Design
{
    class DesignTimeProjectExplorerViewModel : ProjectExplorerViewModel
    {
        DesignTimeProjectExplorerViewModel()
        {
            Root = new FolderItemViewModel("C:\\");
        }
    }
}
