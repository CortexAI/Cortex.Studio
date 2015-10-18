using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Studio.Elements;
using Gemini.Framework;

namespace Cortex.Studio.Modules.ProcessDesigner.ViewModels
{
    public class ElementCalledEventArgs : RoutedEventArgs
    {
        public ElementCalledEventArgs(Flow flow)
        {
            this.Flow = flow;
        }

        public Flow Flow { get; private set; }
    }

    class EditorWrapperViewModel : Document
    {
        private readonly UserControl _editorView;
        private readonly ElementItemDefenition _defenition;
        private readonly EditorViewModel _vm;
        public UserControl EditorView { get { return _editorView; } }

        public override string DisplayName { get { return _defenition.Name; }}

        public EditorWrapperViewModel(ElementItemDefenition itemDefenition, EditorViewModel vm, UserControl view)
        {
            _defenition = itemDefenition;
            _editorView = view;
            _vm = vm;
            ViewModelBinder.Bind(vm, view, null);
        }

        public void Apply()
        {
            if(_vm != null)
                _vm.Apply();
        }
    }
}
