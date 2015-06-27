using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Cortex.Modules.ElementsToolbox.ViewModels;
using Cortex.Modules.Output.ViewModels;
using Cortex.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework;
using Gemini.Modules.Inspector;

namespace Cortex.Modules.Startup
{
    [Export(typeof(IModule))]
    class Module : ModuleBase
    {
        public override IEnumerable<Type> DefaultTools
        {
            get 
            {
                yield return typeof(ProjectExplorerViewModel); 
                yield return typeof(ElementsToolboxViewModel); 
                yield return typeof(IInspectorTool);
                yield return typeof(OutputViewModel); 
            }
        }

        public override void Initialize()
        {
            Shell.ShowFloatingWindowsInTaskbar = true;
            Shell.ToolBars.Visible = true;

            MainWindow.WindowState = WindowState.Maximized;
            MainWindow.Title = "Cortex";
        }
    }
}
