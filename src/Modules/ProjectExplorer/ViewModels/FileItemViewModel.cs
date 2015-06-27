using System;
using System.IO;

namespace Cortex.Modules.ProjectExplorer.ViewModels
{
    public class FileItemViewModel : TreeViewItemBase
    {
        private string _fullPath;
        private string _name;

        public override string Name
        {
            get { return _name; }
        }

        public Uri IconUri
        {
            get { return new Uri("pack://application:,,,/Resources/document_16xLG.png"); }
        }

        public FileItemViewModel(string path)
        {
            _fullPath = path;
            _name = Path.GetFileName(path);
        }
    }
}
