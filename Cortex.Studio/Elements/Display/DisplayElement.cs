using System.Collections.Generic;
using System.IO;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;
using Cortex.Core.Model.Serialization;

namespace Cortex.Studio.Elements
{
    class DisplayElement : IElement
    {
        public IEnumerable<IInputPin> Inputs { get { return _inputs; } }

        public IEnumerable<IOutputPin> Outputs { get { return _outputs; } }


        private readonly IInputPin[] _inputs;
        private readonly IOutputPin[] _outputs;

        public string Value { get; private set; }

        public DisplayElement()
        {
            _inputs = new IInputPin[]
            {
                new FlowInputPin(Action),
                new DataInputPin("Object",typeof(object)),
            };
            _outputs = new IOutputPin[]
            {
                new FlowOutputPin("Out")
            };
        }

        private void Action(Flow flow)
        {
            var o = ((DataInputPin) _inputs[1]).Value;
            Value = o != null ? o.ToString() : "null";
            ((FlowOutputPin)_outputs[0]).Call(flow);
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public void Deserialize(BinaryReader reader)
        {
            Value = reader.ReadString();
        }

        public void Save(IPersisterWriter writer)
        {
            writer.Set("Value", Value);
        }

        public void Load(IPersisterReader reader)
        {
            reader.Get<string>("Value");
        }
    }
}
