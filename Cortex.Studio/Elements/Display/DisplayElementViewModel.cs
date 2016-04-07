using Cortex.Core.Model;

namespace Cortex.Studio.Elements.Display
{
    class DisplayElementViewModel : EditorViewModel
    {
        private string _value;

        public override string Name => "Display";
        public string Value
        {
            get
            {
                return _value;
            }
            private set
            {
                _value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public DisplayElementViewModel(INode element) : base(element)
        {
        }

       

        public override void AfterInputPinProcessed(IInputPin input, object o)
        {
            Value = o.ToString();
        }
    }
}
