using System.Windows.Media;

namespace Cortex.Modules.ProjectExplorer.ViewModels
{
    public class FileItemViewModel : TreeViewItemBase
    {
        private readonly string _fullPath;
        private readonly string _name;

        public override string Name
        {
            get { return _name; }
        }
        
        public ImageSource Icon { get; private set; }

        public override string Path { get { return _fullPath; } }

        public FileItemViewModel(string path)
        {
            _fullPath = path;
            _name = System.IO.Path.GetFileName(path);
            this.Icon = Util.FileIcons.GetImageSource(Util.FileIcons.GetSmallIcon(path));
        }
    }
}
