using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cortex.Annotations;
using Cortex.Model;

namespace Cortex.Modules.ElementsToolbox.ViewModels
{
    class ElementDescriptionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IElement _elementTemplate;

        public string Name
        {
            get { return _elementTemplate.Name; }
        }

        public string Description
        {
            get { return _elementTemplate.Description; }
        }

        public Uri IconUri
        {
            get { return _elementTemplate.IconUri; }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ElementDescriptionViewModel(IElement template)
        {
            _elementTemplate = template;
        }

        public IElement Create()
        {
            return Activator.CreateInstance(_elementTemplate.GetType()) as IElement;
        }
    }
}
