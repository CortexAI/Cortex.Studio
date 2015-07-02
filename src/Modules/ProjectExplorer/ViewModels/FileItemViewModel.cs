using System;
using System.IO;

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

        public Uri IconUri
        {
            get { return new Uri("pack://application:,,,/Resources/document_16xLG.png"); }
        }

        public override string Path { get { return _fullPath; } }

        public FileItemViewModel(string path)
        {
            _fullPath = path;
            _name = System.IO.Path.GetFileName(path);
        }
    }
}
