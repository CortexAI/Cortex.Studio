using System;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Cortex.Core.Model;
using Cortex.Studio.Modules.ProcessDesigner.Util;

namespace Cortex.Studio.Modules.ProcessDesigner.ViewModels
{
    public class InputConnectorViewModel : PropertyChangedBase, IConnectorViewModel
    {
        private Point _position;
        private int _connections;
        
        public event EventHandler PositionChanged;

        public IInputPin Pin { get; }

        public ElementViewModel Element { get; }

        public ConnectorDirection ConnectorDirection => ConnectorDirection.Input;

        public string Name => Pin?.Name;
        public Color Color => TypeToColorConverter.GetColor(Pin);

        public Type Type => Pin?.Type;

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

        public bool IsConnected => _connections > 0;

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
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}