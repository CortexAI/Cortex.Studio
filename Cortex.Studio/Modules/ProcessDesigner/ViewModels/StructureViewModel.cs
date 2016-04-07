using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Cortex.Core.Model;
using Gemini.Framework;
using Gemini.Framework.Services;

namespace Cortex.Studio.Modules.ProcessDesigner.ViewModels
{
    [Export(typeof(StructureViewModel))]
    class StructureViewModel : Tool
    {
        private IContainer _process;
        private INode _selectedItem;
        private ObservableCollection<INode> _elements;
        private ObservableCollection<IConnection> _connections;

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Right; }
        }

        public ObservableCollection<INode> Elements
        {
            get { return _elements; }
            set
            {
                _elements = value;
                NotifyOfPropertyChange(() => Elements);
            }
        }


        public INode SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(()=>SelectedItem);

                if (_selectedItem != null)
                {
                    var metaData = Container.GetMetaData(SelectedItem);
                    if (metaData != null)
                    {
                        MetaData = new ObservableCollection<KeyValuePair<string, object>>(metaData);
                    }
                    NotifyOfPropertyChange(() => MetaData);
                }
            }
        }

        public ObservableCollection<KeyValuePair<string,Object>> MetaData { get; set; }

        public ObservableCollection<IConnection> Connections
        {
            get { return _connections; }
            set
            {
                _connections = value;
                NotifyOfPropertyChange(() => Connections);
            }
        }

        public IContainer Container
        {
            get { return _process; }
            set
            {
                if (_process != null)
                {
                    _process.ConnectionAdded -= ProcessOnConnectionAdded;
                    _process.ConnectionRemoved -= ProcessOnConnectionRemoved;
                    _process.ElementAdded -= ProcessOnElementAdded;
                    _process.ElementRemoved -= ProcessOnElementRemoved;
                }

                _process = value;
                if(_process == null)
                    return;

                NotifyOfPropertyChange(() => Container);

                Elements = new ObservableCollection<INode>(_process.Elements);
                Connections = new ObservableCollection<IConnection>(_process.Connections);

                _process.ConnectionAdded+= ProcessOnConnectionAdded;
                _process.ConnectionRemoved += ProcessOnConnectionRemoved;
                _process.ElementAdded += ProcessOnElementAdded;
                _process.ElementRemoved += ProcessOnElementRemoved;
            }
        }

        private void ProcessOnElementRemoved(IContainer container, INode element)
        {
            Elements.Remove(element);
        }

        private void ProcessOnElementAdded(IContainer container, INode element)
        {
            Elements.Add(element);
        }

        private void ProcessOnConnectionRemoved(IContainer container, IConnection connection)
        {
            Connections.Remove(connection);
        }

        private void ProcessOnConnectionAdded(IContainer container, IConnection connection)
        {
            Connections.Add(connection);
        }

        [ImportingConstructor]
        public StructureViewModel(IShell shell)
        {
            DisplayName = "Container structure";
            shell.ActiveDocumentChanged += ShellOnActiveDocumentChanged;
            GetFromShell(shell);
        }

        private void ShellOnActiveDocumentChanged(object sender, EventArgs eventArgs)
        {
            var shell = sender as IShell;
            if (shell != null)
                GetFromShell(shell);
        }

        private void GetFromShell(IShell shell)
        {
            var doc = shell.ActiveItem;
            var processDesigner = doc as GraphViewModel;
            if (processDesigner != null)
            {
                Container = processDesigner.Process;
            }
        }
    }
}
