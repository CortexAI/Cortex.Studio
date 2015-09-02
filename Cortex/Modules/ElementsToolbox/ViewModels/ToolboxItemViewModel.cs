using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cortex.Annotations;

namespace Cortex.Modules.ElementsToolbox.ViewModels
{
    public abstract class ToolboxItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Uri IconUri { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
