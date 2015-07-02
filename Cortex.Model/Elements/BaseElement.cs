using System;

namespace Cortex.Model.Elements
{
    [Serializable]
    public abstract class BaseElement : IElement
    {
        public abstract string Category { get; }
        public abstract string Name { get; }
        public abstract Uri IconUri { get; }
        public abstract string Description { get; }
        public InputPin[] Inputs { get; protected set; }
        public OutputPin[] Outputs { get; protected set; }

        protected BaseElement()
        {
        }
    }
}
