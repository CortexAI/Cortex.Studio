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
                if (_from != null)
                {
                    _from.Detach(this);
                    _from.PositionChanged -= OnFromPositionChanged;
                }
                _from = value;
                if (_from != null)
                {
                    _from.Attach(this);
                    _from.PositionChanged += OnFromPositionChanged;
                }
                NotifyOfPropertyChange(() => From);
            }
        }

        private InputConnectorViewModel _to;
        public InputConnectorViewModel To
        {
            get { return _to; }
            private set
            {
                if (_to != null)
                {
                    _to.Detach(this);
                    _to.PositionChanged -= OnToPositionChanged;
                }
                _to = value;
                if (_to != null)
                {
                    _to.Attach(this);
                    _to.PositionChanged += OnToPositionChanged;
                }
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
        
        public ConnectionViewModel(OutputConnectorViewModel from, InputConnectorViewModel to, IConnection connection)
        {
            From = from;
            To = to;
            
            Connection = connection;
        }

        public ConnectionViewModel(OutputConnectorViewModel from)
        {
            From = from;
        }

        public void Attach(InputConnectorViewModel to)
        {
            To = to;

            if(Connection == null && To != null && From != null)
                Connection = new Connection(From.Element.Element, From.Pin, To.Element.Element, To.Pin);
            else
                throw new Exception("Can't create connection");
        }

        private void OnFromPositionChanged(object sender, EventArgs e)
        {
            FromPosition = ((IConnectorViewModel)sender).Position;
        }

        private void OnToPositionChanged(object sender, EventArgs e)
        {
            ToPosition = ((IConnectorViewModel)sender).Position;
        }

        public void Clear()
        {
            To = null;
            From = null;
        }
    }
}