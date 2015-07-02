using Cortex.Model;
using Cortex.Model.Elements;
using Cortex.Modules.ProcessDesigner.ViewModels;

namespace Cortex.Modules.ProcessDesigner.Design
{
    public class DesignTimeGraphViewModel : GraphViewModel
    {
        public DesignTimeGraphViewModel() : base(null)
        {
            var element1 = new ElementViewModel(new AdditionElement()) { X = 100, Y = 200 };
            var element3 = new ElementViewModel(new DivisionElement()) { X = 300, Y = 300 };

            Elements.Add(element1);
            Elements.Add(element3);
            Connections.Add(new ConnectionViewModel(element1.OutputConnectors[0], element3.InputConnectors[0]));
            element1.IsSelected = true;
        }
    }
}
