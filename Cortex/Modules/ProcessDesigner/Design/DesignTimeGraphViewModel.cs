using Cortex.Elements;
using Cortex.Model;
using Cortex.Modules.ProcessDesigner.ViewModels;

namespace Cortex.Modules.ProcessDesigner.Design
{
    public class DesignTimeGraphViewModel : GraphViewModel
    {
        public DesignTimeGraphViewModel()
        {
            var process = new Process();
            var element1 = new ElementViewModel(process, EditorElementsDefenition.LogElement.CreateElement()) { X = 100, Y = 200 };
            var element2 = new ElementViewModel(process, EditorElementsDefenition.LogElement.CreateElement()) { X = 400, Y = 100 };
            var element3 = new ElementViewModel(process, EditorElementsDefenition.LogElement.CreateElement()) { X = 300, Y = 300 };
            
            
            Elements.Add(element1);
            Elements.Add(element2);
            Elements.Add(element3);
            //Connections.Add(new ConnectionViewModel(element1.OutputConnectors[0], element3.InputConnectors[0]));
            element1.IsSelected = true;
        }
    }
}
