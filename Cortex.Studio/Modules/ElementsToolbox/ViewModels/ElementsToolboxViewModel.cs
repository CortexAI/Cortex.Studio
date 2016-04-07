using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Cortex.Core.Model;
using Gemini.Framework;
using Gemini.Framework.Services;

namespace Cortex.Studio.Modules.ElementsToolbox.ViewModels
{
    [Export(typeof(ElementsToolboxViewModel))]
    class ElementsToolboxViewModel : Tool
    {
        public static string DataFormat = "ElementDescriptionViewModel";

        private ObservableCollection<CategoryViewModel> _categories;

        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyOfPropertyChange(() => Categories);
            }
        }

        public override PaneLocation PreferredLocation => PaneLocation.Left;

        public override string DisplayName => "Elements Toolbox";

        public ElementsToolboxViewModel()
        {
            var categories = IoC.GetAll<NodeGroupDefenition>().Select(g => new CategoryViewModel(g)).ToList();
            var elements = IoC.GetAll<NodeDefenition>().ToList();

            foreach (var egroup in elements.GroupBy(e => e.Group))
            {
                var category = categories.FirstOrDefault(cat => cat.GroupDefenition.Equals(egroup.Key));
                if(category != null)
                    category.Items = new ObservableCollection<ToolboxItemViewModel>(egroup.Select(elem => new ElementItemViewModel(elem)));
            }

            foreach (var category in categories)
            {
                if (category.GroupDefenition.ParentGroup != null)
                {
                    var parent = categories.FirstOrDefault(
                            cat => cat.GroupDefenition.Equals(category.GroupDefenition.ParentGroup));

                    parent?.Items.Insert(0, category);
                }
            }

            Categories = new ObservableCollection<CategoryViewModel>(categories.Where(cat => cat.GroupDefenition.ParentGroup == null));
        }

        public void OnMouseDown(object sender, object dataContext, EventArgs args)
        {
            var vm = dataContext as ElementItemViewModel;
            var elem = sender as FrameworkElement;
            if (vm != null && elem != null)
            {
                var dragData = new DataObject(DataFormat, elem.DataContext);
                DragDrop.DoDragDrop(elem, dragData, DragDropEffects.Copy);
            }
        }
    }
}
