using System;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements
{
    [Serializable]
    public abstract class BaseElement : IElement
    {
        public abstract string Category { get; }
        public abstract string Name { get; }
        public abstract Uri IconUri { get; }
        public abstract string Description { get; }
        public IInputPin[] Inputs { get; protected set; }
        public IOutputPin[] Outputs { get; protected set; }

        protected BaseElement()
        {
        }
    }
}
