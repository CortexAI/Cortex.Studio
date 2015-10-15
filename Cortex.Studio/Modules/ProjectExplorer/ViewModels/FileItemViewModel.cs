using System.Windows.Media;
using Caliburn.Micro;
using Cortex.Studio.Modules.ProjectExplorer.Util;
using Gemini.Framework.Services;

namespace Cortex.Studio.Modules.ProjectExplorer.ViewModels
{
    public class FileItemViewModel : TreeViewItemBase
    {
        private readonly string _fullPath;
        private readonly string _name;
        private IEditorProviderSelector _providerSelector;

        public override string Name
        {
            get { return _name; }
        }

        public IEditorProvider EditorProvider { get; private set; }

        public bool IsEditorAvailable { get { return EditorProvider != null; } }
        
        public ImageSource Icon { get; private set; }

        public override string Path { get { return _fullPath; } }

        public FileItemViewModel(string path)
        {
            _fullPath = path;
            _name = System.IO.Path.GetFileName(path);
            this.Icon = Util.FileIcons.GetImageSource(Util.FileIcons.GetSmallIcon(path));
            _providerSelector = IoC.Get<IEditorProviderSelector>();
            EditorProvider = _providerSelector.GetEditor(path);
        }
    }
}
