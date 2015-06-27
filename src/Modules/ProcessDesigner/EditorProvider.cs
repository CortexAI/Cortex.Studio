using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;
using Cortex.Modules.ProcessDesigner.ViewModels;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Inspector;

namespace Cortex.Modules.ProcessDesigner
{
    [Export(typeof(IEditorProvider))]
    [Export(typeof(EditorProvider))]
    public class EditorProvider : IEditorProvider
    {
        public bool Handles(string path)
        {
            var extension = Path.GetExtension(path);
            return extension == ".prcs";
        }

        public IDocument CreateNew(string name)
        {
            return new GraphViewModel(IoC.Get<IInspectorTool>());
        }

        public IDocument Open(string path)
        {
            return new GraphViewModel(IoC.Get<IInspectorTool>());
        }

        public IEnumerable<EditorFileType> FileTypes
        {
            get
            {
                yield return new EditorFileType("Process", ".prcs");
            }
        }
    }
}
