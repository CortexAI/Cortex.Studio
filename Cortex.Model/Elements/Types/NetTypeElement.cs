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
                if(Outputs != null)
                    return (T)Outputs[0].Value;
                return default(T);
            }
            set
            {
                if (Outputs != null)
                    Outputs[0].Value = value;
            }
        }

        public NetTypeElement()
        {
            Value = default(T);
            Outputs = new []
            {
                new OutputPin("Output", typeof(T), default(T))
            };
        }
    }
}