using System.Collections.Generic;

namespace Cortex.Studio.Modules.ProjectExplorer.Services
{
    interface IRecentProjectsPersister
    {
        int MaxProjects { get; set; }
        IEnumerable<string> RecentProjects { get; }
        void AddProject(string path);
        void RemoveProject(string path);
        void Clear();
    }
}