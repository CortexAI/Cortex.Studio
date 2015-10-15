using System;
using System.Windows.Controls;
using Cortex.Model;

namespace Cortex.Studio.Elements
{
    class ElementItemWithViewDefenition<T1,T2> : ElementItemDefenition<T1>, IViewProvider
        where T1 : IElement, new() 
        where T2 : UserControl, new()
    {
        public ElementItemWithViewDefenition(ElementGroupDefenition group, string name, Uri icon, string description) 
            : base(group, name, icon, description)
        {
        }

        public UserControl View
        {
            get { return new T2(); }
        }
    }
}
