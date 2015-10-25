using System;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Cortex.Core.Model;
using Cortex.Core.Model.Pins;
using Cortex.Studio.Modules.ProcessDesigner.Util;

namespace Cortex.Studio.Modules.ProcessDesigner.ViewModels
{
    public class InputConnectorViewModel : PropertyChangedBase, IConnectorViewModel
    {
        private Point _position;
        private int _connections;
        
        public event EventHandler PositionChanged;

        public IInputPin Pin { get; private set; }

        public ElementViewModel Element { get; private set; }

        public ConnectorDirection ConnectorDirection
        {
            get { return ConnectorDirection.Input; }
        }

        public string Name { get { return Pin.Name; } }
        
        public Color Color
        {
            get { return TypeToColorConverter.GetColor(Pin); }
        }

        public Type Type
        {
            get
            {
                var dPin = Pin as IDataPin;
                if (dPin != null)
                    return dPin.Type;
                return typeof (Flow);
            }
        }

        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                NotifyOfPropertyChange(() => Position);
                RaisePositionChanged();
            }
        }

        public bool IsConnected
        {
            get { return _connections > 0; }
        }
        
        public void Attach(ConnectionViewModel connection)
        {
            _connections++;
            NotifyOfPropertyChange(() => IsConnected);
        }

        public void Detach(ConnectionViewModel connection)
        {
            _connections--;
            NotifyOfPropertyChange(() => IsConnected);
        }
        
        public InputConnectorViewModel(ElementViewModel element, IInputPin pin)
        {
            Pin = pin;
            Element = element;
        }

        private void RaisePositionChanged()
        {
            var handler = PositionChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}