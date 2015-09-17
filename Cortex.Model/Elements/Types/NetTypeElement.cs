using Cortex.Model.Pins;
using Cortex.Model.Serialization;

namespace Cortex.Model.Elements.Types
{
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

        public override void Save(IPersisterWriter writer)
        {
            writer.Set("Value", Value);
        }

        public override void Load(IPersisterReader reader)
        {
            Value = reader.Get<T>("Value");
        }
    }
}