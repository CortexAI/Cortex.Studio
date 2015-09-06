using System;
using Cortex.Model.Utilities;

namespace Cortex.Model.Pins
{
    [Serializable]
    public class DataInputPin : IDataInputPin
    {
        private IDataOutputPin _connected;
        
        public string Name { get; private set; }

        public object Value
        {
            get
            {
                if(_connected != null)
                    return _connected.Value;
                return null;
            }
        }

        public Type Type { get; private set; }

        public void Attach(IOutputPin pin)
        {
            var inPin = pin as IDataOutputPin;
            if(inPin == null)
                throw new Exception("Pin is not a data pin");

            // If is not convertable to base type
            if (!inPin.Type.IsCastableTo(Type))
                throw new Exception("Pin type mismatch");

            _connected = inPin;
        }

        public void Detach(IOutputPin pin)
        {
            _connected = null;
        }

        public DataInputPin(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}