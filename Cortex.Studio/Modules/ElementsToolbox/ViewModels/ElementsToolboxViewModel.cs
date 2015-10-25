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

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Left; }
        }

        public override string DisplayName { get { return "Elements Toolbox"; }}

        public ElementsToolboxViewModel()
        {
            var elements = IoC.GetAll<ElementItemDefenition>();
            var categroiesPool = elements.GroupBy(e => e.Group).Select(g => new CategoryViewModel(g.Key, g)).ToList();

            foreach (var category in categroiesPool.ToList())
            {
                if (category.GroupDefenition.ParentGroup != null)
                {
                    var parent =
                        categroiesPool.FirstOrDefault(
                            cat => cat.GroupDefenition.Equals(category.GroupDefenition.ParentGroup));
                    
                    if (parent == null)
                    {
                        parent = new CategoryViewModel(category.GroupDefenition.ParentGroup,
                            new List<ElementItemDefenition>());
                        parent.Items.Add(category);
                        categroiesPool.Add(parent);
                    }

                     parent.Items.Insert(0, category);
                }
            }

            Categories = new ObservableCollection<CategoryViewModel>(categroiesPool.Where(cat => cat.GroupDefenition.ParentGroup == null));
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
