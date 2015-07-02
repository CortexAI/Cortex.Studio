using System;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Cortex.Model;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class InputConnectorViewModel : PropertyChangedBase, IConnectorViewModel, IDisposable
    {
        private ConnectionViewModel _connection;
        private Point _position;
        
        public event EventHandler SourceChanged;
        public event EventHandler PositionChanged;

        public InputPin Pin { get; private set; }

        public ElementViewModel Element { get; private set; }

        public ConnectorDirection ConnectorDirection
        {
            get { return ConnectorDirection.Input; }
        }

        public string Name { get { return Pin.Name; } }

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
        
        public ConnectionViewModel Connection
        {
            get { return _connection; }
            set
            {
                if(_connection == null)
                    Pin.SetSourcePin(null);
                if (_connection != null)
                    _connection.From.Element.OutputChanged -= OnSourceElementOutputChanged;
                _connection = value;
                if (_connection != null)
                    _connection.From.Element.OutputChanged += OnSourceElementOutputChanged;
                RaiseSourceChanged();
                Pin.SetSourcePin(Connection.From.Pin);
                NotifyOfPropertyChange(() => Connection);
            }
        }

        private void OnSourceElementOutputChanged(object sender, EventArgs e)
        {
            RaiseSourceChanged();
        }

        public InputConnectorViewModel(ElementViewModel element, InputPin pin)
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
            Pin.SetSourcePin(null);
        }
    }
}