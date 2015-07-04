using Cortex.Modules.ProjectExplorer.Commands;
using Gemini.Framework.Commands;

namespace Cortex.Modules.ProjectExplorer.Services
{
    interface IProjectService : ICommandHandler<OpenFolderDefenition>
    {
        bool IsProjectLoaded { get; }
        string ProjectPath { get; }
        IRecentProjectsPersister Persister { get; }
        void Open(string path);
        void OpenRecent();
    }
}
