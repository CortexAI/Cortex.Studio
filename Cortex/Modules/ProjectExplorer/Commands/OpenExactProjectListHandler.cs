using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Modules.ProjectExplorer.Services;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;

namespace Cortex.Modules.ProjectExplorer.Commands
{
    [CommandHandler]
    class OpenExactProjectListHandler : ICommandListHandler<OpenExactProjectListDefenition>
    {
        private IProjectService _projectService;

        [ImportingConstructor]
        public OpenExactProjectListHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public void Populate(Command command, List<Command> commands)
        {
            if(_projectService == null)
                return;

            var recent = _projectService.Persister.RecentProjects;
            foreach (var path in recent)
            {
                commands.Add(new Command(command.CommandDefinition)
                {
                    Text = path,
                    Tag = path,
                });
            }
        }

        public Task Run(Command command)
        {
            _projectService.Open(command.Tag as string);
            return TaskUtility.Completed;
        }
    }
}
