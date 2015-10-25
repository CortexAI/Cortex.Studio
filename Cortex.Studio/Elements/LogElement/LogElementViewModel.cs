using System.Linq;
using Caliburn.Micro;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Studio.Elements
{
    class LogElementViewModel : EditorViewModel
    {
        public IObservableCollection<string> Log { get; private set; }
        
        public LogElementViewModel(IElement element) : base(element)
        {
            Log = new BindableCollection<string>();
        }
        
        public override void AfterElementCalled(Flow flow)
        {
            var val = ((DataInputPin) Element.Inputs.ToArray()[1]).Value.ToString();
            Log.Add(val);
        }
    }
}
