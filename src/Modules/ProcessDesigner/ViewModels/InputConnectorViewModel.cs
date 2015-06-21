using System;
using System.Windows.Media;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class InputConnectorViewModel : ConnectorViewModel
    {
        public event EventHandler SourceChanged;

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
                if (_connection != null)
                    _connection.From.Element.OutputChanged -= OnSourceElementOutputChanged;
                _connection = value;
                if (_connection != null)
                    _connection.From.Element.OutputChanged += OnSourceElementOutputChanged;
                RaiseSourceChanged();
                NotifyOfPropertyChange(() => Connection);
            }
        }

        private void OnSourceElementOutputChanged(object sender, EventArgs e)
        {
            RaiseSourceChanged();
        }

        public InputConnectorViewModel(ElementViewModel element, string name, Color color)
            : base(element, name, color)
        {
            
        }

        private void RaiseSourceChanged()
        {
            var handler = SourceChanged;
            if (handler!= null)
                handler(this, EventArgs.Empty);
        }
    }
}