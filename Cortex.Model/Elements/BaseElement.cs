using System;
using System.Collections.Generic;
using Cortex.Model.Pins;
using Cortex.Model.Serialization;

namespace Cortex.Model.Elements
{
    public abstract class BaseElement : IElement
    {
        protected readonly List<IInputPin> _inputs;
        protected readonly List<IOutputPin> _outputs;

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

        protected T GetInputData<T>(int index)
        {
            object val = ((IDataInputPin) _inputs[index]).Value;
            if (val == null)
                return default(T);
            if (val is T)
                return (T) val;
            return (T) Convert.ChangeType(val, typeof (T));
        }
    }
}