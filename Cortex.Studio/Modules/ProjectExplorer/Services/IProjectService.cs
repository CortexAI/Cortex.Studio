using Cortex.Studio.Modules.ProjectExplorer.Commands;
using Gemini.Framework.Commands;

namespace Cortex.Studio.Modules.ProjectExplorer.Services
{
    interface IProjectService : ICommandHandler<OpenProjectDefenition>
    {
        bool IsProjectLoaded { get; }
        string ProjectPath { get; }
        IRecentProjectsPersister Persister { get; }
        void Open(string path);
        void OpenRecent();
    }
}
