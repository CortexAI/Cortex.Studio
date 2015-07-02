using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using Caliburn.Micro;
using Cortex.Modules.ProcessDesigner.ViewModels;
using Cortex.Modules.ProjectExplorer.ViewModels;
using Gemini.Framework;
using Gemini.Framework.Services;

namespace Cortex.Modules.ProcessDesigner
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

        public IDocument CreateNew(string name)
        {
            var explorer = IoC.Get<ProjectExplorerViewModel>();
            return new GraphViewModel(Path.Combine(explorer.Root.Path, name));
        }

        public IDocument Open(string path)
        {
            try
            {
                var formatter = new SoapFormatter();
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var doc = formatter.Deserialize(stream) as GraphViewModel;
                    stream.Close();

                    if (doc != null)
                    {
                        _log.Info("Opened document: {0}", path);
                        doc.FileName = path;
                        return doc;
                    }
                }                
            }
            catch (Exception exception)
            {
                _log.Error(exception);
            }

            return null;
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
