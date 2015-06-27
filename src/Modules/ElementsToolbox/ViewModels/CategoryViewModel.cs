using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cortex.Annotations;
using Cortex.Model;

namespace Cortex.Modules.ElementsToolbox.ViewModels
{
    class CategoryViewModel : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(Name);
            }
        }

        public ObservableCollection<ElementDescriptionViewModel> Elements { get; set; }
        
        public CategoryViewModel(string name, IEnumerable<IElement> elements)
        {
            Name = name;
            var elementsVm = new ObservableCollection<ElementDescriptionViewModel>();

            foreach (var element in elements)
            {
                elementsVm.Add(new ElementDescriptionViewModel(element));
            }


            Elements = elementsVm;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
