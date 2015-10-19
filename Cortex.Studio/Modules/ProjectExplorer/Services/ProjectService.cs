using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cortex.Studio.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;

namespace Cortex.Studio.Modules.ProjectExplorer.Services
{
    [CommandHandler]
    [Export(typeof(IProjectService))]
    class ProjectService : IProjectService
    {
        
        private readonly ProjectExplorerViewModel _vm;
        private const string DefaultFolderName = "Cortex Projects";

        [ImportingConstructor]
        public ProjectService(ProjectExplorerViewModel vm)
        {
            _vm = vm;
            Persister = new RegistryRecentProjectsPersister
            {
                MaxProjects = 5
            };
        }

        public void Update(Command command)
        {
        }

        public Task Run(Command command)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath =
                    System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        DefaultFolderName)
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                Open(dialog.SelectedPath);
            return TaskUtility.Completed;
        }

        public bool IsProjectLoaded { get { return !string.IsNullOrEmpty(ProjectPath); } }
        
        public string ProjectPath { get; private set; }
        
        public IRecentProjectsPersister Persister { get; private set; }
        
        /// <summary>
        /// Opens or creates project
        /// </summary>
        /// <param name="path"></param>
        public void Open(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                if(Persister.RecentProjects.Contains(path))
                    Persister.RemoveProject(path);
            }

            _vm.Open(path);
            ProjectPath = path;
            Persister.AddProject(path);
        }
        
        public void OpenRecent()
        {
            var recent = Persister.RecentProjects.ToArray();
            if (recent.Length > 0)
            {
                // Open recent project
                Open(recent[0]);
            }
            else
            {
                // Open default path
                var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DefaultFolderName);
                
                var exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);

                Open(path);
            }
        }
    }
}
