using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Cortex.Model;
using Gemini.Framework;
using Gemini.Framework.Services;

namespace Cortex.Modules.ElementsToolbox.ViewModels
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

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Left; }
        }

        public ElementsToolboxViewModel()
        {
            var categories = new ObservableCollection<CategoryViewModel>();
            DisplayName = "Elements Toolbox";

            var elements = IoC.GetAll<IElement>();
            foreach (var group in elements.GroupBy(e => e.Category))
            {
                categories.Add(new CategoryViewModel(group.Key, group));
            }

            Categories = categories;
        }

        public void OnMouseDown(object sender, object dataContext, EventArgs args)
        {
            var vm = dataContext as ElementDescriptionViewModel;
            var elem = sender as FrameworkElement;
            if (vm != null && elem != null)
            {
                var dragData = new DataObject(DataFormat, elem.DataContext);
                DragDrop.DoDragDrop(elem, dragData, DragDropEffects.Copy);
            }
        }
    }
}
