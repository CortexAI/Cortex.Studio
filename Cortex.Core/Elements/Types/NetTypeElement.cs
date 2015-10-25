using Cortex.Core.Model.Pins;
using Cortex.Core.Model.Serialization;

namespace Cortex.Core.Elements.Types
{
    public class NetTypeElement<T> : BaseElement
    {
        private readonly DataOutputPin<T> _value = new DataOutputPin<T>("Value");

        public NetTypeElement()
        {
            AddOutputPin(_value);
            Value = default(T);
        }

        public T Value
        {
            get { return _value.Value; }
            set { _value.Value = value; }
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