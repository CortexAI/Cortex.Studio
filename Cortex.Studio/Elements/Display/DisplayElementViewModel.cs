using Cortex.Core.Model;

namespace Cortex.Studio.Elements
{
    class DisplayElementViewModel : EditorViewModel
    {
        private string _value;

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

        public DisplayElementViewModel(IElement element) : base(element)
        {
        }

        public override void AfterElementCalled(Flow flow)
        {
            Value = ((DisplayElement) Element).Value;
        }
    }
}
