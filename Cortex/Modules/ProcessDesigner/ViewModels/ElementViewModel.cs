using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Pins;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    [Serializable]
    public class ElementViewModel : PropertyChangedBase, ISerializable
    {
        private bool _isSelected;
        private readonly BindableCollection<InputConnectorViewModel> _inputConnectors = new BindableCollection<InputConnectorViewModel>();
        private readonly BindableCollection<OutputConnectorViewModel> _outputConnectors = new BindableCollection<OutputConnectorViewModel>();
        
        private readonly IElement _element;
        private double _x;
        private double _y;

        public event EventHandler OutputChanged;

        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                NotifyOfPropertyChange(() => X);
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                NotifyOfPropertyChange(() => Y);
            }
        }

        public string Name
        {
            get
            {
                return _element != null ? _element.Name : "Element";
            }
        }

        public Uri IconUri
        {
            get
            {
                return _element != null ? _element.IconUri : null;
            }
        }

        public IElement Element
        {
            get { return _element; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }
        
        public IList<InputConnectorViewModel> InputConnectors
        {
            get { return _inputConnectors; }
        }

        public IList<OutputConnectorViewModel> OutputConnectors
        {
            get { return _outputConnectors; }
        }

        public IEnumerable<ConnectionViewModel> AttachedConnections
        {
            get
            {
                return _inputConnectors.SelectMany(x => x.Connections)
                    .Union(_outputConnectors.SelectMany(x => x.Connections))
                    .Where(x => x != null);
            }
        }
        
        public ElementViewModel(IElement element)
        {
            _element = element;
            SetConnectors();
        }

        protected ElementViewModel(SerializationInfo info, StreamingContext context)
        {
            _element = (IElement)info.GetValue("Element", typeof(IElement));
            _x = info.GetDouble("X");
            _y = info.GetDouble("Y");
            SetConnectors();
        }

        private void SetConnectors()
        {
            if (_element.Inputs != null)
                foreach (var pin in _element.Inputs)
                    AddInputConnector(pin);

            if (_element.Outputs != null)
                foreach (var pin in _element.Outputs)
                    AddOutputConnector(pin);
        }

        protected void AddInputConnector(IInputPin pin)
        {
            var inputConnector = new InputConnectorViewModel(this, pin);
            inputConnector.SourceChanged += InputConnectorOnSourceChanged;
            _inputConnectors.Add(inputConnector);
        }

        private void InputConnectorOnSourceChanged(object sender, EventArgs eventArgs)
        {

        }

        protected void AddOutputConnector(IOutputPin pin)
        {
            var outputConnector = new OutputConnectorViewModel(this, pin);
            _outputConnectors.Add(outputConnector);
        }

        protected virtual void RaiseOutputChanged()
        {
            var handler = OutputChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Element", _element);
            info.AddValue("X", _x);
            info.AddValue("Y", _y);
        }
    }
}