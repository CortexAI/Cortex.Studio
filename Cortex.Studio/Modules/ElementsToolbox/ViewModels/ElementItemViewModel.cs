using System;
using Cortex.Model;

namespace Cortex.Studio.Modules.ElementsToolbox.ViewModels
{
    class ElementItemViewModel : ToolboxItemViewModel
    {
        private readonly ElementItemDefenition _elementDefenition;

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

        public ElementItemDefenition Defenition
        {
            get { return _elementDefenition; }
        }
        
        public ElementItemViewModel(ElementItemDefenition defenition)
        {
            _elementDefenition = defenition;
        }
    }
}
