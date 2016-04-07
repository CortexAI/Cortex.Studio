using System.Windows.Controls;
using Caliburn.Micro;
using Cortex.Core.Model;
using Cortex.Studio.Elements;
using Gemini.Framework;

namespace Cortex.Studio.Modules.ProcessDesigner.ViewModels
{
    class EditorWrapperViewModel : Document
    {
        private readonly NodeDefenition _defenition;
        private readonly EditorViewModel _vm;
        public UserControl EditorView { get; }

        public override string DisplayName => _defenition.Name;

        public EditorWrapperViewModel(NodeDefenition itemDefenition, EditorViewModel vm, UserControl view)
        {
            _defenition = itemDefenition;
            EditorView = view;
            _vm = vm;
            ViewModelBinder.Bind(vm, view, null);
        }

        public void Apply()
        {
            _vm?.Apply();
        }
    }
}
