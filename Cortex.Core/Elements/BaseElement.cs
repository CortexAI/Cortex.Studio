using System.Collections.Generic;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;
using Cortex.Core.Model.Serialization;

namespace Cortex.Core.Elements
{
    public abstract class BaseElement : IElement
    {
        private readonly List<IInputPin> _inputs;
        private readonly List<IOutputPin> _outputs;

        protected BaseElement()
        {
            _inputs = new List<IInputPin>();
            _outputs = new List<IOutputPin>();
        }

        public IEnumerable<IInputPin> Inputs
        {
            get { return _inputs; }
        }

        public IEnumerable<IOutputPin> Outputs
        {
            get { return _outputs; }
        }

        public virtual void Save(IPersisterWriter writer)
        {
        }

        public virtual void Load(IPersisterReader reader)
        {
        }

        protected void AddInputPin(IInputPin pin)
        {
            _inputs.Add(pin);
        }

        protected void AddOutputPin(IOutputPin pin)
        {
            _outputs.Add(pin);
        }
    }
}