using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cortex.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;

namespace Cortex.Modules.ProjectExplorer.Services
{
    [CommandHandler]
    [Export(typeof(IProjectService))]
    class ProjectService : IProjectService
    {
        [Import]
        private ProjectExplorerViewModel _vm;

        private List<string> _recentProjects;

        public ProjectService()
        {
            Persister = new RegistryRecentProjectsPersister {MaxProjects = 5};
        }

        public void Update(Command command)
        {
        }

        public Task Run(Command command)
        {
            var dialog = new FolderBrowserDialog();
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                Open(dialog.SelectedPath);
            return TaskUtility.Completed;
        }

        public bool IsProjectLoaded { get { return !string.IsNullOrEmpty(ProjectPath); } }
        public string ProjectPath { get; private set; }
        public IRecentProjectsPersister Persister { get; private set; }
        public void Open(string path)
        {
            _vm.Open(path);
            ProjectPath = path;
            Persister.AddProject(path);
        }

        public void OpenRecent()
        {
            var recent = Persister.RecentProjects.ToArray();
            if (recent.Length > 0)
                Open(recent[0]);
        }
    }
}
