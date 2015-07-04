using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;
using Cortex.Modules.ProjectExplorer.Services;
using Gemini.Framework;
using Gemini.Framework.Services;

namespace Cortex.Modules.ProjectExplorer.ViewModels
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
        private readonly IEditorProvider[] _editorProviders;

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
        
        [ImportingConstructor]
        public ProjectExplorerViewModel(IShell shell, [ImportMany] IEditorProvider[] editorProviders)
        {
            DisplayName = "Project Explorer";
            _shell = shell;
            _editorProviders = editorProviders;
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
        private void Open(FileItemViewModel file)
        {
            var editor = _editorProviders.FirstOrDefault(e => e.Handles(file.Path));
            if (editor != null)
            {
                _shell.OpenDocument(editor.Open(file.Path));
                _log.Info("Opening {0} with {1}", file.Path, editor.ToString());
            }
            else
            {
                _log.Warn("Can't find editor for {0}", file.Path);
            }
        }
    }
}
