using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Cortex.Model.Pins;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class InputConnectorViewModel : PropertyChangedBase, IConnectorViewModel, IDisposable
    {
        private readonly List<ConnectionViewModel> _connections = new List<ConnectionViewModel>();
        private Point _position;
        
        public event EventHandler SourceChanged;
        public event EventHandler PositionChanged;

        public IInputPin Pin { get; private set; }

        public ElementViewModel Element { get; private set; }

        public ConnectorDirection ConnectorDirection
        {
            get { return ConnectorDirection.Input; }
        }

        public string Name { get { return Pin.Name; } }

        public bool AllowMultipleConnections { get { return Pin.AllowMultipleConnections; } }

        public IList<ConnectionViewModel> Connections { get { return _connections; }}

        public Color Color
        {
            get { return TypeToColorConverter.GetColor(Pin.Type); }
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
            get { return _connections.Count > 0; }
        }
        
        public void Attach(ConnectionViewModel connection)
        {
            connection.From.Element.OutputChanged += OnSourceElementOutputChanged;
            _connections.Add(connection);
            Pin.Attach(connection.From.Pin);
            NotifyOfPropertyChange(() => IsConnected);
        }

        public void Detach(ConnectionViewModel connection)
        {
            connection.From.Element.OutputChanged -= OnSourceElementOutputChanged;
            _connections.Remove(connection);
            Pin.Detach(connection.From.Pin);
            NotifyOfPropertyChange(() => IsConnected);
        }

        public void DetachAll()
        {
            foreach (var connectionViewModel in _connections)
            {
                connectionViewModel.From.Element.OutputChanged -= OnSourceElementOutputChanged;
            }
            Pin.DetachAll();
            _connections.Clear();
            NotifyOfPropertyChange(() => IsConnected);
        }

        private void OnSourceElementOutputChanged(object sender, EventArgs e)
        {
            RaiseSourceChanged();
        }

        public InputConnectorViewModel(ElementViewModel element, IInputPin pin)
        {
            Pin = pin;
            Element = element;
        }

        private void RaiseSourceChanged()
        {
            var handler = SourceChanged;
            if (handler!= null)
                handler(this, EventArgs.Empty);
        }

        private void RaisePositionChanged()
        {
            var handler = PositionChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Pin.DetachAll();
        }
    }
}