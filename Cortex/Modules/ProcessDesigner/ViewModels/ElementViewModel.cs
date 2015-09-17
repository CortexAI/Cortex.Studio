using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Caliburn.Micro;
using Cortex.Elements;
using Cortex.Model;
using Cortex.Model.Pins;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class ElementViewModel : PropertyChangedBase
    {
        private bool _isSelected;
        private readonly BindableCollection<InputConnectorViewModel> _inputConnectors = new BindableCollection<InputConnectorViewModel>();
        private readonly BindableCollection<OutputConnectorViewModel> _outputConnectors = new BindableCollection<OutputConnectorViewModel>();
        
        private readonly IElement _element;
        private readonly ElementItemDefenition _itemDefenition;
        private readonly UserControl _view;

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
                return _element != null ? _itemDefenition.Name : "Element";
            }
        }

        public Uri IconUri
        {
            get
            {
                return _element != null ? _itemDefenition.IconUri : null;
            }
        }

        public UserControl View
        {
            get { return _view; }
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
        
        public ElementViewModel(IContainer process, IElement element)
        {
            _element = element;
            var defenitionType = process.GetMetaData<Type>(element, "Defenition");
            _itemDefenition = IoC.GetAll<ElementItemDefenition>().FirstOrDefault(d => d.GetType() == defenitionType);

            if(_itemDefenition == null)
                throw new Exception("No defenition");

            X = process.GetMetaData<double>(element, "X");
            Y = process.GetMetaData<double>(element, "Y");
            
            var defenitionWithView = _itemDefenition as IViewProvider;
            if (defenitionWithView != null)
            {
                _view = defenitionWithView.View;
                _view.DataContext = _element;
            }

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
            _inputConnectors.Add(inputConnector);
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
    }
}