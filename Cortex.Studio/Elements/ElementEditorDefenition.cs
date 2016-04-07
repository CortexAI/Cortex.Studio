using System;
using System.Windows.Controls;
using Cortex.Core.Model;

namespace Cortex.Studio.Elements
{
    public abstract class ElementEditorDefenition : IViewProvider
    {
        public Type ElementType { get; protected set; }
        public bool Embed { get; protected set; }
        public abstract UserControl CreateView();
        public abstract EditorViewModel CreateViewModel(INode element);
    }

    public class ElementEditor<T1, T2, T3> : ElementEditorDefenition
        where T1 : INode
        where T2 : EditorViewModel
        where T3 : UserControl, new()
    {
        public ElementEditor()
        {
            ElementType = typeof (T1);
        }

        public ElementEditor(bool embed) : this()
        {
            Embed = embed;
        }
        
        public override UserControl CreateView()
        {
            return new T3();
        }

        public override EditorViewModel CreateViewModel(INode element)
        {
            return Activator.CreateInstance(typeof(T2), element) as EditorViewModel;
        }
    }
}