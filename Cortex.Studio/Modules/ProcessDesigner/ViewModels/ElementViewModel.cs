using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Pins;
using Cortex.Studio.Elements;
using Cortex.Studio.Modules.ProcessDesigner.Commands;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace Cortex.Studio.Modules.ProcessDesigner.ViewModels
{
    public class ElementViewModel : PropertyChangedBase, ICommandHandler<OpenEditorCommandDefenition>
    {
        private bool _isSelected;
        private readonly BindableCollection<InputConnectorViewModel> _inputConnectors = new BindableCollection<InputConnectorViewModel>();
        private readonly BindableCollection<OutputConnectorViewModel> _outputConnectors = new BindableCollection<OutputConnectorViewModel>();
        
        private readonly IElement _element;
        private readonly ElementItemDefenition _itemDefenition;
        private readonly IEnumerable<ElementEditorDefenition> _editors;

        private double _x;
        private double _y;
        private EditorWrapperViewModel _attachedEditorWrapper;
        private bool _isShowingEmbedded;


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

        public UserControl EmbeddedView { get; private set; }

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

        public bool IsShowingEmbedded
        {
            get { return _isShowingEmbedded; }
            set
            {
                _isShowingEmbedded = value; 
                NotifyOfPropertyChange(()=>IsShowingEmbedded);
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
            _editors = IoC.GetAll<ElementEditorDefenition>().Where(e => e.ElementType == _element.GetType());

            if(_itemDefenition == null)
                throw new Exception("No defenition");

            X = process.GetMetaData<double>(element, "X");
            Y = process.GetMetaData<double>(element, "Y");


            var embeddedEditor = _editors.FirstOrDefault(e => e.Embed);
            if (embeddedEditor != null)
            {
                var embeddedView = embeddedEditor.CreateView();
                ViewModelBinder.Bind(embeddedEditor.CreateViewModel(Element), embeddedView, null);
                this.EmbeddedView = embeddedView;
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

        public void Update(Command command)
        {
            
        }

        public Task Run(Command command)
        {
            OpenEditor();
            return TaskUtility.Completed;
        }

        public void OpenEditor()
        {
            if (_editors != null && _editors.Any())
            {
                if (_attachedEditorWrapper == null)
                {
                    var editor = _editors.First();
                    _attachedEditorWrapper = new EditorWrapperViewModel(_itemDefenition, editor.CreateViewModel(this.Element), editor.CreateView());
                }
                IoC.Get<IShell>().OpenDocument(_attachedEditorWrapper);
            }
        }

        public void MouseDown(MouseButtonEventArgs args)
        {
            if (args.ClickCount >= 2)
            {
                OpenEditor();
                args.Handled = true;
            }
        }
    }
}