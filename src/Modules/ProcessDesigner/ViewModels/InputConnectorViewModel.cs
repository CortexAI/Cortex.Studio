using System;
using Cortex.Model;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class InputConnectorViewModel : ConnectorViewModel, IDisposable
    {
        public event EventHandler SourceChanged;
        public InputPin Pin { get; private set; }

        public override ConnectorDirection ConnectorDirection
        {
            get { return ConnectorDirection.Input; }
        }

        private ConnectionViewModel _connection;
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
            : base(element, pin.Name, TypeToColorConverter.GetColor(pin.Type))
        {
            Pin = pin;
        }

        private void RaiseSourceChanged()
        {
            var handler = SourceChanged;
            if (handler!= null)
                handler(this, EventArgs.Empty);
        }


        public void Dispose()
        {
            Pin.SetSourcePin(null);
        }
    }
}