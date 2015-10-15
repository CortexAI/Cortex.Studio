using System.ComponentModel.Composition;
using System.Linq;
using Gemini.Framework.Services;

namespace Cortex.Studio.Modules.ProjectExplorer.Util
{
    [Export(typeof(IEditorProviderSelector))]
    class EditorProviderSelector : IEditorProviderSelector
    {
        private readonly IEditorProvider[] _editors;

        [ImportingConstructor]
        public EditorProviderSelector([ImportMany] IEditorProvider[] editors)
        {
            _editors = editors;
        }

        public IEditorProvider GetEditor(string path)
        {
            return _editors.FirstOrDefault(e => e.Handles(path));
        }
    }

    interface IEditorProviderSelector
    {
        IEditorProvider GetEditor(string path);
    }
}
