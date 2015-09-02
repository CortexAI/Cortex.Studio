using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cortex.Annotations;
using Cortex.Model;

namespace Cortex.Modules.ElementsToolbox.ViewModels
{
    class ElementItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ElementItemDefenition _elementDefenition;

        public string Name
        {
            get { return _elementDefenition.Name; }
        }

        public string Description
        {
            get { return _elementDefenition.Description; }
        }

        public Uri IconUri
        {
            get { return _elementDefenition.IconUri; }
        }

        public ElementItemDefenition Defenition
        {
            get { return _elementDefenition; }
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ElementItemViewModel(ElementItemDefenition defenition)
        {
            _elementDefenition = defenition;
        }
    }
}
