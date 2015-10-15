using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Studio.Modules.Output.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Studio.Modules.Output.Commands
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
