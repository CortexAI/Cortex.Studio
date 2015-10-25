using System;
using System.Collections.ObjectModel;
using Cortex.Core.Model;

namespace Cortex.Studio.Modules.ElementsToolbox.ViewModels
{
    class CategoryViewModel : ToolboxItemViewModel
    {
        private readonly ElementGroupDefenition _groupDefenition;

        public override string Name
        {
            get { return _groupDefenition.Name; }
        }

        public override string Description
        {
            get { return null; }
        }

        public override Uri IconUri
        {
            get { return null; }
        }

        public ElementGroupDefenition GroupDefenition
        {
            get { return _groupDefenition; }
        }

        public ObservableCollection<ToolboxItemViewModel> Items { get; set; }
       
        public CategoryViewModel(ElementGroupDefenition @group)
        {
            _groupDefenition = group;
            Items = new ObservableCollection<ToolboxItemViewModel>();
        }
    }
}
