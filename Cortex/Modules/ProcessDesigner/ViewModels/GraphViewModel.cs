using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Pins;
using Cortex.Model.Serialization;
using Cortex.Modules.ProcessDesigner.Commands;
using Gemini.Framework;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using Gemini.Modules.Inspector;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    [Export(typeof(GraphViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GraphViewModel : PersistedDocument, 
        ICommandHandler<RunProcessCommandDefenition>,
        ICommandHandler<StopProcessCommandDefenition>
    {
        private readonly IInspectorTool _inspectorTool;
        private readonly ILog _log = LogManager.GetLog(typeof (GraphViewModel));
        private Executor _executor;
        private IContainer _process;
        private readonly BindableCollection<ElementViewModel> _elements = new BindableCollection<ElementViewModel>();

        public IObservableCollection<ElementViewModel> Elements
        {
            get { return _elements; }
        }
        
        private readonly BindableCollection<ConnectionViewModel> _connections = new BindableCollection<ConnectionViewModel>();

        public IObservableCollection<ConnectionViewModel> Connections
        {
            get { return _connections; }
        }

        public IEnumerable<ElementViewModel> SelectedElements
        {
            get { return _elements.Where(x => x.IsSelected); }
        }

        public bool IsRunning
        {
            get
            {
                return _executor != null && _executor.IsRunning;
            }
        }

        public IContainer Process { get { return _process; } }

        public GraphViewModel()
        {
            _inspectorTool = IoC.Get<IInspectorTool>();
        }

        public ConnectionViewModel OnConnectionDragStarted(IConnectorViewModel sourceConnector, Point currentDragPoint)
        {
            if (!(sourceConnector is OutputConnectorViewModel))
                return null;

            var connection = new ConnectionViewModel((OutputConnectorViewModel)sourceConnector)
            {
                ToPosition = currentDragPoint
            };

            Connections.Add(connection);
            return connection;
        }

        public void OnConnectionDragging(Point currentDragPoint, ConnectionViewModel connection)
        {
            // If current drag point is close to an input connector, show its snapped position.
            var nearbyConnector = FindNearbyInputConnector(currentDragPoint);
            connection.ToPosition = (nearbyConnector != null)
                ? nearbyConnector.Position
                : currentDragPoint;
        }

        public void OnConnectionDragCompleted(Point currentDragPoint, ConnectionViewModel newConnection, IConnectorViewModel sourceConnector)
        {
            var nearbyConnector = FindNearbyInputConnector(currentDragPoint);

            // No target connector found or target is a source
            if (nearbyConnector == null || sourceConnector.Element == nearbyConnector.Element)
            {
                Connections.Remove(newConnection);
                newConnection.Clear();
                _log.Warn("No target for connection was found or target is a source");
                return;
            }

            // Connection already exist
            if (Connections.FirstOrDefault(c => c.From == sourceConnector && c.To == nearbyConnector) != null)
            {
                Connections.Remove(newConnection);
                newConnection.Clear();
                _log.Warn("That connection already exists");
                return;
            }

            try
            {
                // Datapins couldn't have multiple connections
                if (nearbyConnector.IsConnected && nearbyConnector.Pin is IDataPin)
                {
                    var connections = Connections.Where(c => c.To == nearbyConnector).ToList();
                    foreach (var connection in connections)
                    {
                        Connections.Remove(connection);
                        connection.Clear();
                        _process.RemoveConnection(connection.Connection);
                    }
                    _log.Warn("Target connector doesn't support multiple inputs. Removing other connectors");
                }
                newConnection.Attach(nearbyConnector);
                _process.AddConnection(newConnection.Connection);
                _log.Info("Connection created");
            }
            catch (Exception ex)
            {
                Connections.Remove(newConnection);
                newConnection.Clear();
                _log.Error(ex);
            }
        }

        private InputConnectorViewModel FindNearbyInputConnector(Point mousePosition)
        {
            return Elements.SelectMany(x => x.InputConnectors)
                .FirstOrDefault(x => AreClose(x.Position, mousePosition, 10));
        }

        private static bool AreClose(Point point1, Point point2, double distance)
        {
            return (point1 - point2).Length < distance;
        }

        public void DeleteElement(ElementViewModel element)
        {
            var connections = 
                        Connections.Where(c => c.From.Element == element)
                .Union( Connections.Where(c => c.To.Element == element)).ToList();

            foreach (var connection in connections)
            {
                _process.RemoveConnection(connection.Connection);
                Connections.Remove(connection);
            }
            _process.RemoveElement(element.Element);
            Elements.Remove(element);
        }

        public void DeleteSelectedElements()
        {
            Elements.Where(x => x.IsSelected)
                .ToList()
                .ForEach(DeleteElement);
        }

        public void OnSelectionChanged()
        {
            var selectedElements = SelectedElements.ToList();

            if (selectedElements.Count == 1)
                _inspectorTool.SelectedObject = new InspectableObjectBuilder()
                    .WithObjectProperties(selectedElements[0].Element, x => true)
                    .ToInspectableObject();
            else
                _inspectorTool.SelectedObject = null;
        }
        
        void ICommandHandler<RunProcessCommandDefenition>.Update(Command command)
        {
            command.Enabled = !IsRunning;
        }

        Task ICommandHandler<RunProcessCommandDefenition>.Run(Command command)
        {
            _executor.Start();
            return TaskUtility.Completed;
        }

        void ICommandHandler<StopProcessCommandDefenition>.Update(Command command)
        {
            command.Enabled = IsRunning;
        }

        Task ICommandHandler<StopProcessCommandDefenition>.Run(Command command)
        {
            _executor.Stop();
            return TaskUtility.Completed;
        }

        protected override Task DoNew()
        {
            _log.Info("Graph created: {0}", FileName);
            _process = new Process();
            _executor = new Executor((Process)_process);
            return TaskUtility.Completed;
        }

        protected override Task DoLoad(string filePath)
        {
            _process = ContainerPersister.Deserialize<Process>(filePath);

            foreach (var element in _process.Elements)
            {
                Elements.Add(new ElementViewModel(_process, element));
            }

            foreach (var connection in _process.Connections)
            {
                var startElement = Elements.FirstOrDefault(e => e.Element.Equals(connection.StartElement));
                var endElement = Elements.FirstOrDefault(e => e.Element.Equals(connection.EndElement));
                var startConnector = startElement.OutputConnectors.FirstOrDefault(c => c.Pin.Equals(connection.StartPin));
                var endConnector = endElement.InputConnectors.FirstOrDefault(c => c.Pin.Equals(connection.EndPin));
                Connections.Add(new ConnectionViewModel(startConnector, endConnector, connection));
            }

            _executor = new Executor((Process)_process);
            return TaskUtility.Completed;
        }

        protected override Task DoSave(string filePath)
        {
            foreach (var elementViewModel in _elements)
            {
                _process.SetMetaData(elementViewModel.Element, "X", elementViewModel.X);
                _process.SetMetaData(elementViewModel.Element, "Y", elementViewModel.Y);
            }
            ContainerPersister.Serialize(_process, filePath);
            return TaskUtility.Completed;
        }

        public void CreateElement(ElementItemDefenition defenition, Point mousePosition)
        {
            var element = defenition.CreateElement();
            _process.AddElement(element);
            _process.SetMetaData(element, "Defenition", defenition.GetType());
            _process.SetMetaData(element, "X", mousePosition.X);
            _process.SetMetaData(element, "Y", mousePosition.Y);
            Elements.Add(new ElementViewModel(_process, element));
        }
    }
}
