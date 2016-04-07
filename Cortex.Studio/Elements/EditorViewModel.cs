using System;
using Caliburn.Micro;
using Cortex.Core.Model;

namespace Cortex.Studio.Elements
{
    public abstract class EditorViewModel : PropertyChangedBase, IDisposable
    {
        public abstract string Name { get; }
        public INode Element { get; }

        protected EditorViewModel(INode element)
        {
            Element = element;
            foreach (var pin in element.Inputs)
            {
               // pin.AfterPinProcessed += AfterInputPinProcessed;
            }
        }

        public virtual void AfterInputPinProcessed(IInputPin input, object o) { }

        public virtual void Apply() { }
        public void Dispose()
        {
            if (Element != null)
            {
                foreach (var pin in Element.Inputs)
                {
                    //pin.AfterPinProcessed -= AfterInputPinProcessed;
                }
            }
        }
    }
}
