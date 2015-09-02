using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cortex.Model;

namespace Cortex.Modules.ElementsToolbox.ViewModels
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
        
        public CategoryViewModel(ElementGroupDefenition @group, IEnumerable<ElementItemDefenition> elements)
        {
            _groupDefenition = group;
            var items = elements.Select(e => new ElementItemViewModel(e)).ToList();
            Items = new ObservableCollection<ToolboxItemViewModel>(items);
        }
    }
}
