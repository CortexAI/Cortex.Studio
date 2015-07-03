using System;
using System.Collections.Generic;

namespace Cortex.Model.Pins
{
    [Serializable]
    public class InputPin : IInputPin
    {
        private IOutputPin _connectedPin;
        private readonly object _defaultValue;
       
        public string Name { get; protected set; }
        public Type Type { get; protected set; }
        public IEnumerable<IOutputPin> Connected { get { return new[] { _connectedPin }; } }
        public bool IsConnected { get { return _connectedPin != null; } }
        public bool AllowMultipleConnections { get { return false; } }
        public Object Value
        {
            get
            {
                if (_connectedPin != null)
                    return _connectedPin.Value;
                return _defaultValue;
            }
        }

        public InputPin(string name, Type t, object defaultValue)
        {
            Name = name;
            Type = t;
            _defaultValue = defaultValue;
        }

        public void Attach(IOutputPin pin)
        {
            if (pin == null)
            {
                _connectedPin = null;
                return;
            }
            
            if(!Type.IsAssignableFrom(pin.Type))
                throw new Exception("Type mismatch");
            _connectedPin = pin;
        }

        public void Detach(IOutputPin pin)
        {
            if (_connectedPin != null && _connectedPin.Equals(pin))
            {
                _connectedPin = null;
                return;
            }
            throw new InvalidOperationException("Trying to detach not attached pin");
        }

        public void DetachAll()
        {
            _connectedPin = null;
        }
    }
}