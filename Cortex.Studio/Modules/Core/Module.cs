using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Cortex.Studio.Modules.ElementsToolbox.ViewModels;
using Cortex.Studio.Modules.Output.ViewModels;
using Cortex.Studio.Modules.ProcessDesigner.ViewModels;
using Cortex.Studio.Modules.ProjectExplorer.Services;
using Cortex.Studio.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework;
using Gemini.Modules.Inspector;

namespace Cortex.Studio.Modules.Core
{
    [Export(typeof(IModule))]
    class Module : ModuleBase
    {
        private readonly IProjectService _projectService;

        public override IEnumerable<Type> DefaultTools
        {
            get 
            {
                yield return typeof(ProjectExplorerViewModel); 
                yield return typeof(ElementsToolboxViewModel); 
                yield return typeof(IInspectorTool);
                yield return typeof(OutputViewModel);
                yield return typeof(StructureViewModel); 
            }
        }

        [ImportingConstructor]
        public Module(IProjectService service)
        {
            _projectService = service;
        }

        public override void Initialize()
        {
            Shell.ShowFloatingWindowsInTaskbar = true;
            Shell.ToolBars.Visible = true;

            MainWindow.WindowState = WindowState.Maximized;
            MainWindow.Title = "Cortex Studio";

            // TODO: open lastproject
            _projectService.OpenRecent();
        }
    }
}
