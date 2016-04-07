using System;
using Cortex.Core.Model;

namespace Cortex.Studio.Modules.ElementsToolbox.ViewModels
{
    class ElementItemViewModel : ToolboxItemViewModel
    {
        private readonly NodeDefenition _elementDefenition;

        public override string Name
        {
            get { return _elementDefenition.Name; }
        }

        public override string Description
        {
            get { return _elementDefenition.Description; }
        }

        public override Uri IconUri
        {
            get { return _elementDefenition.IconUri; }
        }

        public NodeDefenition Defenition
        {
            get { return _elementDefenition; }
        }
        
        public ElementItemViewModel(NodeDefenition defenition)
        {
            _elementDefenition = defenition;
        }
    }
}
