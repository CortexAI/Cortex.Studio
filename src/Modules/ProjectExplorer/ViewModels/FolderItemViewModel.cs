using System.Collections.ObjectModel;
using System.IO;

namespace Cortex.Modules.ProjectExplorer.ViewModels
{
    public class FolderItemViewModel : TreeViewItemBase
    {
        private readonly string _path;
        private readonly string _name;

        public override string Name
        {
            get { return _name; }
        }

        private ObservableCollection<TreeViewItemBase> _childs;

        public ObservableCollection<TreeViewItemBase> Childs
        {
            get
            {
                if (_childs == null)
                    _childs = GetChilds();
                return _childs;
            }
        }

        public FolderItemViewModel(string path)
        {
            _path = path;
            _name = Path.GetFileName(_path);
        }

        private ObservableCollection<TreeViewItemBase> GetChilds()
        {
            var childs = new ObservableCollection<TreeViewItemBase>();
            foreach (var dirName in Directory.GetDirectories(_path))
            {
                childs.Add(new FolderItemViewModel(dirName));
            }
            foreach (var fileName in Directory.GetFiles(_path))
            {
                childs.Add(new FileItemViewModel(fileName));
            }
            return childs;
        }
    }
}
