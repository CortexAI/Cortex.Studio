using System;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Cortex.Model;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class ConnectionViewModel : PropertyChangedBase
    {
        private OutputConnectorViewModel _from;
        public OutputConnectorViewModel From
        {
            get { return _from; }
            private set
            {
                _from = value;
                NotifyOfPropertyChange(() => From);
            }
        }

        private InputConnectorViewModel _to;
        public InputConnectorViewModel To
        {
            get { return _to; }
            private set
            {
                _to = value;
                NotifyOfPropertyChange(() => To);
            }
        }
        

        private Point _fromPosition;
        public Point FromPosition
        {
            get { return _fromPosition; }
            set
            {
                _fromPosition = value;
                NotifyOfPropertyChange(() => FromPosition);
            }
        }
        

        private Point _toPosition;
        public Point ToPosition
        {
            get { return _toPosition; }
            set
            {
                _toPosition = value;
                NotifyOfPropertyChange(() => ToPosition);
            }
        }
        
        public Color Color
        {
            get { return _from.Color; }
        }

        public IConnection Connection { get; private set; }
        
        public ConnectionViewModel(OutputConnectorViewModel from, InputConnectorViewModel to, IConnection connection = null)
        {
            From = from;
            From.PositionChanged += OnFromPositionChanged;

            To = to;
            To.PositionChanged += OnToPositionChanged;

            if(connection == null)
                SetConnection();
            else
                Connection = connection;
        }

        private void SetConnection()
        {
            if (To == null || From == null)
                Connection = null;
            else
                Connection = new Connection(From.Element.Element, From.Pin, To.Element.Element, To.Pin);
        }

        public ConnectionViewModel(OutputConnectorViewModel from)
        {
            From = from;
            From.PositionChanged += OnFromPositionChanged;
        }

        public void Attach(InputConnectorViewModel to, IContainer process)
        {
            if (To != null)
            {
                To.Detach(this);
                To.PositionChanged -= OnToPositionChanged;
            }
            To = to;
            To.PositionChanged += OnToPositionChanged;
            To.Attach(this);
            if (Connection != null)
                process.RemoveConnection(Connection);

            SetConnection();
            if(Connection != null)
                process.AddConnection(Connection);
        }

        private void OnFromPositionChanged(object sender, EventArgs e)
        {
            FromPosition = ((IConnectorViewModel)sender).Position;
        }

        private void OnToPositionChanged(object sender, EventArgs e)
        {
            ToPosition = ((IConnectorViewModel)sender).Position;
        }
    }
}