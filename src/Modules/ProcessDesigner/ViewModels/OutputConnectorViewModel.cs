using Caliburn.Micro;
using Cortex.Model;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class OutputConnectorViewModel : ConnectorViewModel
    {
        public OutputPin Pin { get; private set; }
        public override ConnectorDirection ConnectorDirection
        {
            get { return ConnectorDirection.Output; }
        }

        private readonly BindableCollection<ConnectionViewModel> _connections;
        public IObservableCollection<ConnectionViewModel> Connections
        {
            get { return _connections; }
        }

        public OutputConnectorViewModel(ElementViewModel element, OutputPin pin)
            : base(element, pin.Name, TypeToColorConverter.GetColor(pin.Type))
        {
            Pin = pin;
            _connections = new BindableCollection<ConnectionViewModel>();
        }
    }
}