using System;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements
{
    [Serializable]
    public class NetTypeElement<T> : BaseElement
    {
        public override string Description { get { return string.Format("{0}. Represents default .NET type", typeof (T).FullName); } }

        public override string Category { get { return "Types"; } }

        public override string Name { get { return typeof (T).Name; } }

        public override Uri IconUri { get { return null; } }

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