using System.Linq;
using Caliburn.Micro;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;

namespace Cortex.Studio.Elements
{
    public abstract class EditorViewModel : PropertyChangedBase
    {
        public IElement Element { get; private set; }

        protected EditorViewModel(IElement element)
        {
            Element = element;
            foreach (var pin in element.Outputs.OfType<IFlowOutputPin>())
            {
                pin.Called += AfterElementCalled;
            }
        }

        public virtual void AfterElementCalled(Flow flow) { }

        public virtual void Apply() { }
    }
}
