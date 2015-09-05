using System;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.Types
{
    [Serializable]
    public class NetTypeElement<T> : BaseElement
    {
        public T Value
        {
            get
            {
                if (((IDataOutputPin)_outputs[0]).Value != null)
                    return (T)((IDataOutputPin)_outputs[0]).Value;
                return default(T);
            }
            set
            {
                ((DataOutputPin)_outputs[0]).Value = value;
            }
        }

        public NetTypeElement()
        {
            AddOutputPin(new DataOutputPin("Value", typeof(T)));
            Value = default(T);
        }
    }
}