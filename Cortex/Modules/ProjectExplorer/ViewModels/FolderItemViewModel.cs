using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

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

        public override string Path { get { return _path; } }

        private ObservableCollection<TreeViewItemBase> _childs;

        public ObservableCollection<TreeViewItemBase> Childs
        {
            get { return _childs ?? (_childs = GetChilds()); }
        }

        public FolderItemViewModel(string path)
        {
            _path = path;
            _name = System.IO.Path.GetFileName(_path);
        }

        private ObservableCollection<TreeViewItemBase> GetChilds()
        {
            var childs = new ObservableCollection<TreeViewItemBase>();
            try
            {
                foreach (var dirName in Directory.GetDirectories(_path))
                {
                    childs.Add(new FolderItemViewModel(dirName));
                }
                foreach (var fileName in Directory.GetFiles(_path))
                {
                    childs.Add(new FileItemViewModel(fileName));
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
            
            return childs;
        }

        public void Refresh()
        {
            _childs = GetChilds();
            /*
            var folders = _childs.OfType<FolderItemViewModel>();
            var files = _childs.OfType<FileItemViewModel>();

            foreach (var dirName in Directory.GetDirectories(_path))
            {
                var dir = folders.FirstOrDefault(f => f.Path == dirName);
                if(dir != null)
                    dir.Refresh();
                else
                    _childs.Add(new FolderItemViewModel(dirName));
            }

            foreach (var fileName in Directory.GetFiles(_path))
            {
                var file = files.FirstOrDefault(f => f.Path == fileName);
                if(file == null)
                    _childs.Add(new FileItemViewModel(fileName));
            }*/
        }
    }
}
