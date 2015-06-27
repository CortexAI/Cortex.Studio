using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using Gemini.Framework;
using Gemini.Framework.Services;

namespace Cortex.Modules.ProjectExplorer.ViewModels
{
    [Export(typeof(ProjectExplorerViewModel))]
    class ProjectExplorerViewModel : Tool
    {
        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Left; }
        }

        public ObservableCollection<TreeViewItemBase> Items
        {
            get
            {
                if(_root != null)
                    return _root.Childs;
                return null;
            }
        }

        private FolderItemViewModel _root;

        public FolderItemViewModel Root
        {
            get { return _root; }
            set
            {
                _root = value;
                NotifyOfPropertyChange(() => Root);
                NotifyOfPropertyChange(() => Items);
            }
        }

        public ProjectExplorerViewModel()
        {
            DisplayName = "Project Explorer";
        }

        public void Open(string path)
        {
            Root = new FolderItemViewModel(Path.GetDirectoryName(path));
        }
    }
}
