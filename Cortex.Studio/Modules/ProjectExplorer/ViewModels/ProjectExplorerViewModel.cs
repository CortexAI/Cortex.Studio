using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Caliburn.Micro;
using Cortex.Studio.Modules.ProjectExplorer.Services;
using Cortex.Studio.Modules.ProjectExplorer.Util;
using Gemini.Framework;
using Gemini.Framework.Services;

namespace Cortex.Studio.Modules.ProjectExplorer.ViewModels
{
    [Export(typeof(ProjectExplorerViewModel))]
    class ProjectExplorerViewModel : Tool
    {
        private readonly ILog _log = LogManager.GetLog(typeof(ProjectExplorerViewModel));

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
        private readonly IShell _shell;
        private readonly IEditorProviderSelector _editorProviderSelector;

        [Import]
        private IProjectService _service;

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

        public bool ShowAllFiles { get; set; }
        
        [ImportingConstructor]
        public ProjectExplorerViewModel(IShell shell, IEditorProviderSelector editorProviderSelector)
        {
            DisplayName = "Project Explorer";
            _shell = shell;
            _editorProviderSelector = editorProviderSelector;

            this.ToolBarDefinition = ProjectExplorer.ToolBarDefenitions.ProjectExplorerToolBar;
        }

        public void Open(string directory)
        {
            Root = new FolderItemViewModel(directory);
        }

        public void OnMouseDown(object source, FileItemViewModel fileItem, MouseButtonEventArgs args)
        {
            if (args.LeftButton == MouseButtonState.Pressed && args.ClickCount == 2)
            {
                Open(fileItem);
            }
        }

        private async void Open(FileItemViewModel file)
        {
            if(!file.IsEditorAvailable)
                _log.Warn("Can't find editor for {0}", file.Path);
            else
            {
                var editor = file.EditorProvider;
                var vm = editor.Create();

                _log.Info("Opening {0} with {1}", file.Path, editor.ToString());
                await editor.Open(vm, file.Path);
                _shell.OpenDocument(vm);
            }
        }

        public void UpdateTree()
        {
            Root.Refresh();
            NotifyOfPropertyChange(() => Root);
            NotifyOfPropertyChange(() => Items);
        }
    }
}
