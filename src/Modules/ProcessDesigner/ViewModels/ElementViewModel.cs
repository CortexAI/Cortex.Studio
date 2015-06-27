using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using Caliburn.Micro;
using Cortex.Model;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class ElementViewModel : PropertyChangedBase
    {
        public event EventHandler OutputChanged;

        private double _x;

        [Browsable(false)]
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                NotifyOfPropertyChange(() => X);
            }
        }

        private double _y;

        [Browsable(false)]
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                NotifyOfPropertyChange(() => Y);
            }
        }

        private string _name;

        [Browsable(false)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private bool _isSelected;

        public Uri IconUri { get; set; }

        public IElement Element { get { return _element; } }

        [Browsable(false)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }
        
        private readonly BindableCollection<InputConnectorViewModel> _inputConnectors;
        public IList<InputConnectorViewModel> InputConnectors
        {
            get { return _inputConnectors; }
        }

        private readonly BindableCollection<OutputConnectorViewModel> _outputConnectors;
        private readonly IElement _element;

        public IList<OutputConnectorViewModel> OutputConnectors
        {
            get { return _outputConnectors; }
        }

        public IEnumerable<ConnectionViewModel> AttachedConnections
        {
            get
            {
                return _inputConnectors.Select(x => x.Connection)
                    .Union(_outputConnectors[0].Connections)
                    .Where(x => x != null);
            }
        }

        public ElementViewModel(IElement element)
        {
            _element = element;
            _inputConnectors = new BindableCollection<InputConnectorViewModel>();
            _outputConnectors = new BindableCollection<OutputConnectorViewModel>();
            _name = element.Name;
            IconUri = element.IconUri;
            
            if(_element.Inputs != null)
            foreach (var pin in _element.Inputs)
                AddInputConnector(pin);

            if (_element.Outputs != null)
            foreach (var pin in _element.Outputs)
                AddOutputConnector(pin);
        }

        protected void AddInputConnector(InputPin pin)
        {
            var inputConnector = new InputConnectorViewModel(this, pin);
            inputConnector.SourceChanged += InputConnectorOnSourceChanged;
            _inputConnectors.Add(inputConnector);
        }

        private void InputConnectorOnSourceChanged(object sender, EventArgs eventArgs)
        {

        }

        protected void AddOutputConnector(OutputPin pin)
        {
            var outputConnector = new OutputConnectorViewModel(this, pin);
            _outputConnectors.Add(outputConnector);
        }

        protected virtual void RaiseOutputChanged()
        {
            EventHandler handler = OutputChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}