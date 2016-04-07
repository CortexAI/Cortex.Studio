using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using Caliburn.Micro;
using Cortex.Studio.Modules.ProcessDesigner.ViewModels;
using Gemini.Framework;
using Gemini.Framework.Services;

namespace Cortex.Studio.Modules.ProcessDesigner
{
    [Export(typeof(IEditorProvider))]
    [Export(typeof(EditorProvider))]
    public class EditorProvider : IEditorProvider
    {
        private ILog _log = LogManager.GetLog(typeof (EditorProvider));

        public bool Handles(string path)
        {
            var extension = Path.GetExtension(path);
            return extension == ".prcs";
        }

        public IDocument Create()
        {
            return IoC.Get<GraphViewModel>();
        }

        public async Task New(IDocument document, string name)
        {
            var graph = ((GraphViewModel) document);

            await graph.New(name);
            
            // TODO: autosave in current project dir
        }

        public async Task Open(IDocument document, string path)
        {
            await ((GraphViewModel) document).Load(path);
        }
        
        public IEnumerable<EditorFileType> FileTypes
        {
            get
            {
                yield return new EditorFileType("ProcessGraph", ".prcs");
            }
        }
    }
}
