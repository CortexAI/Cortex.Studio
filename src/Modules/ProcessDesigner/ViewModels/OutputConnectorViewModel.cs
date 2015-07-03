using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Cortex.Model.Pins;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class OutputConnectorViewModel : PropertyChangedBase, IConnectorViewModel
    {
        private Point _position;
        private readonly BindableCollection<ConnectionViewModel> _connections;

        public event EventHandler PositionChanged;

        public IOutputPin Pin { get; private set; }
        
        public ElementViewModel Element { get; private set; }

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

        public bool IsConnected
        {
            get
            {
                if(Connections != null)
                    return Connections.Count > 0;
                return false;
            }
        }
        
        public ConnectorDirection ConnectorDirection
        {
            get { return ConnectorDirection.Output; }
        }
        
        public IList<ConnectionViewModel> Connections
        {
            get { return _connections; }
        }
        
        public OutputConnectorViewModel(ElementViewModel element, IOutputPin pin)
        {
            Pin = pin;
            Element = element;
            _connections = new BindableCollection<ConnectionViewModel>();
            _connections.CollectionChanged += (sender, args) => NotifyOfPropertyChange(()=>IsConnected);
        }

        private void RaisePositionChanged()
        {
            var handler = PositionChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}