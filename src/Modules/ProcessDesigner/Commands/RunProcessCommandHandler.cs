using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Modules.ProcessDesigner.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Modules.ProcessDesigner.Commands
{
    [CommandHandler]
    class RunProcessCommandHandler : CommandHandlerBase<RunProcessCommandDefenition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public RunProcessCommandHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            var graph = _shell.ActiveItem as GraphViewModel;
            if (graph != null)
            {
                graph.Run();
            }

            return TaskUtility.Completed;
        }
    }
}
