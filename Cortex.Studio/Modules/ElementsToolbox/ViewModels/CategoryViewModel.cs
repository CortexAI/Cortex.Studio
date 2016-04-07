using System;
using System.Collections.ObjectModel;
using Cortex.Core.Model;

namespace Cortex.Studio.Modules.ElementsToolbox.ViewModels
{
    class CategoryViewModel : ToolboxItemViewModel
    {
        private readonly NodeGroupDefenition _groupDefenition;

        public override string Name => _groupDefenition.Name;

        public override string Description => null;

        public override Uri IconUri => null;

        public NodeGroupDefenition GroupDefenition => _groupDefenition;

        public ObservableCollection<ToolboxItemViewModel> Items { get; set; }
       
        public CategoryViewModel(NodeGroupDefenition @group)
        {
            _groupDefenition = group;
            Items = new ObservableCollection<ToolboxItemViewModel>();
        }
    }
}
