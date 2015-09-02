using System;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements
{
    [Serializable]
    public abstract class BaseElement : IElement
    {
        public IInputPin[] Inputs { get; protected set; }
        public IOutputPin[] Outputs { get; protected set; }

        protected BaseElement()
        {
        }
    }
}
