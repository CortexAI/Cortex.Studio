using Cortex.Elements;
using Cortex.Modules.ProcessDesigner.ViewModels;

namespace Cortex.Modules.ProcessDesigner.Design
{
    public class DesignTimeGraphViewModel : GraphViewModel
    {
        public DesignTimeGraphViewModel() : base(null)
        {
            var element1 = new ElementViewModel(EditorElementsDefenition.LogElement) { X = 100, Y = 200 };
            var element2 = new ElementViewModel(EditorElementsDefenition.LogElement) { X = 400, Y = 100 };
            var element3 = new ElementViewModel(EditorElementsDefenition.LogElement) { X = 300, Y = 300 };
            
            
            Elements.Add(element1);
            Elements.Add(element2);
            Elements.Add(element3);
            Connections.Add(new ConnectionViewModel(element1.OutputConnectors[0], element3.InputConnectors[0]));
            element1.IsSelected = true;
        }
    }
}
