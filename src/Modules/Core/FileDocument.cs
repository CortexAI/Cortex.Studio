using System.IO;
using System.Threading.Tasks;
using Cortex.Modules.Core.Commands;
using Gemini.Framework;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;

namespace Cortex.Modules.Core
{
    public class FileDocument : Document, ICommandHandler<SaveCommandDefenition>
    {
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                DisplayName = Path.GetFileName(_fileName);
                NotifyOfPropertyChange(() => FileName);
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        protected FileDocument(string fileName)
        {
            FileName = fileName;
        }

        public virtual void Save() { }

        public void Update(Command command) { }

        public Task Run(Command command)
        {
            if(command.Enabled)
                this.Save();
            return TaskUtility.Completed;
        }
    }
}
