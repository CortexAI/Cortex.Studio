using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Caliburn.Micro;
using Cortex.Modules.ElementsToolbox.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Modules.ElementsToolbox.Commands
{
    [CommandHandler]
    public class OpenElementsToolboxHandler : CommandHandlerBase<OpenElementsToolboxDefinition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public OpenElementsToolboxHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.ShowTool<ElementsToolboxViewModel>();
            return TaskUtility.Completed;
        }
    }
}
