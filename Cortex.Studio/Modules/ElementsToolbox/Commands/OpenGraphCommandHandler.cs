using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Cortex.Studio.Modules.ElementsToolbox.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Studio.Modules.ElementsToolbox.Commands
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
