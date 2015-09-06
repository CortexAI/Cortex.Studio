using System;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    [DataContract(IsReference = true)]
    public class ConnectionViewModel : PropertyChangedBase, IDisposable
    {
        private OutputConnectorViewModel _from;
        [DataMember]
        public OutputConnectorViewModel From
        {
            get { return _from; }
            private set
            {
                if (_from != value)
                {
                    if (_from != null)
                    {
                        _from.Detach(this);
                        _from.PositionChanged -= OnToPositionChanged;
                    }
                }

                _from = value;

                if (_from != null)
                {
                    _from.PositionChanged += OnFromPositionChanged;
                    _from.Attach(this);
                    FromPosition = value.Position;
                }

                NotifyOfPropertyChange(() => From);
            }
        }

        private InputConnectorViewModel _to;
        [DataMember]
        public InputConnectorViewModel To
        {
            get { return _to; }
            set
            {
                if (_to != value)
                {
                    if (_to != null)
                    {
                        _to.Detach(this);
                        _to.PositionChanged -= OnToPositionChanged;
                    }
                }

                _to = value;

                if (_to != null)
                {
                    _to.PositionChanged += OnToPositionChanged;
                    _to.Attach(this);
                    ToPosition = _to.Position;
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
        
        public ConnectionViewModel(OutputConnectorViewModel from, InputConnectorViewModel to)
        {
            From = from;
            To = to;
        }

        public ConnectionViewModel(OutputConnectorViewModel from)
        {
            From = from;
        }

        private void OnFromPositionChanged(object sender, EventArgs e)
        {
            FromPosition = ((IConnectorViewModel)sender).Position;
        }

        private void OnToPositionChanged(object sender, EventArgs e)
        {
            ToPosition = ((IConnectorViewModel)sender).Position;
        }

        public void Remove()
        {
            To = null;
            From = null;
        }

        public void Dispose()
        {
            Remove();
        }
    }
}