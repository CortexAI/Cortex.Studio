using Caliburn.Micro;
using Cortex.Core.Model;

namespace Cortex.Studio.Elements.LogElement
{
    class LogElementViewModel : EditorViewModel
    {
        public IObservableCollection<string> Log { get; }
        public override string Name => "Logger";
        public LogElementViewModel(INode element) : base(element)
        {
            Log = new BindableCollection<string>();
        }
        
        public override void AfterInputPinProcessed(IInputPin input, object o)
        {
            var val = o.ToString();
            Log.Add(val);
            base.AfterInputPinProcessed(input, o);
        }
    }
}
