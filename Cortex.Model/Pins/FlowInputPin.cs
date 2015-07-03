using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cortex.Model.Pins
{
    [Serializable]
    public class FlowInputPin : IInputPin
    {
        private readonly Action _action;
        private IOutputPin[] _connected;

        public string Name { get { return "Flow In"; } }
        public Type Type { get { return typeof(Flow); } }
        public object Value { get { return null; } }

        public IEnumerable<IOutputPin> Connected { get {  return _connected; } }

        public bool IsConnected
        {
            get
            {
                if (_connected == null)
                    return false;
                return _connected.Length != 0;
            }
        }
        public bool AllowMultipleConnections { get { return true; } }
        
        public FlowInputPin(Action action)
        {
            _action = action;
        }
        
        public void Attach(IOutputPin pin)
        {
            var source = pin as FlowOutputPin;
            if (source != null)
            {
                var list = _connected != null ?_connected.ToList() : new List<IOutputPin>();
                list.Add(source);
                _connected = list.ToArray();
                source.Subscribe(_action);
            }
        }

        public void Detach(IOutputPin pin)
        {
            if (_connected != null && _connected.Contains(pin))
            {
                var flow = pin as FlowOutputPin;
                if(flow != null)
                    flow.Unsubscribe(_action);

                var list = _connected.ToList();
                list.Remove(pin);
                _connected = list.ToArray();
            }
            throw new InvalidOperationException("Trying to detach not attached pin");
        }
        
        public void DetachAll()
        {
            if (_connected == null) 
                return;
            foreach (var flow in _connected.OfType<FlowOutputPin>())
                flow.Unsubscribe(_action);
            _connected = null;
        }
    }
}