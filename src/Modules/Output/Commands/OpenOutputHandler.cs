using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Modules.Output.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Modules.Output.Commands
{
    [CommandHandler]
    class OpenOutputHandler : CommandHandlerBase<OpenOutputDefenition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public OpenOutputHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.ShowTool<OutputViewModel>();
            return TaskUtility.Completed;
        }
    }
}
