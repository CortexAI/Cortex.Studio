using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Caliburn.Micro;
using Cortex.Modules.ProcessDesigner.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using Gemini.Modules.Inspector;

namespace Cortex.Modules.ProcessDesigner.Commands
{
    [CommandHandler]
    public class NewProcessCommandHandler : CommandHandlerBase<NewProcessCommandDefinition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public NewProcessCommandHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.OpenDocument(new GraphViewModel(IoC.Get<IInspectorTool>()));
            return TaskUtility.Completed;
        }
    }
}
