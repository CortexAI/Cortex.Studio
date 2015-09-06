using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Cortex.Model.Elements.Logic;
using Cortex.Model.Pins;
using Cortex.Modules.Core;
using Cortex.Modules.ProcessDesigner.Commands;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using Gemini.Modules.Inspector;
using Newtonsoft.Json;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    [Export(typeof(GraphViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GraphViewModel : FileDocument, 
        ICommandHandler<RunProcessCommandDefenition>,
        ICommandHandler<StopProcessCommandDefenition>
    {
        private readonly IInspectorTool _inspectorTool = IoC.Get<IInspectorTool>();
        private readonly ILog _log = LogManager.GetLog(typeof (GraphViewModel));
        
        private readonly BindableCollection<ElementViewModel> _elements = new BindableCollection<ElementViewModel>();
        public IObservableCollection<ElementViewModel> Elements
        {
            get { return _elements; }
        }
        
        private readonly BindableCollection<ConnectionViewModel> _connections = new BindableCollection<ConnectionViewModel>();
        private Thread _thread;

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
                if (_thread == null)
                    return false;
                return _thread.IsAlive;
            }
        }

        public GraphViewModel(string path) : base(path)
        {
            _inspectorTool = IoC.Get<IInspectorTool>();
            _log.Info("Graph created: {0}", FileName);
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
                newConnection.Remove();
                Connections.Remove(newConnection);
                _log.Warn("No target for connection was found or target is a source");
                return;
            }

            // Connection already exist
            if (Connections.FirstOrDefault(c => c.From == sourceConnector && c.To == nearbyConnector) != null)
            {
                newConnection.Remove();
                Connections.Remove(newConnection);
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
                        connection.Remove();
                        Connections.Remove(connection);
                    }
                    _log.Warn("Target connector doesn't support multiple inputs. Removing other connectors");
                }
                newConnection.To = nearbyConnector;
                _log.Info("Connection created");
            }
            catch (Exception ex)
            {
                newConnection.Remove();
                Connections.Remove(newConnection);
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
                connection.Remove();
                Connections.Remove(connection);
            }

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
        
        public override void Save()
        {
            var serializer = new JsonSerializer()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

            using (var sw = new StreamWriter(FileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
                _log.Info("Process saved to {0}", FileName);
            }
        }

        void ICommandHandler<RunProcessCommandDefenition>.Update(Command command)
        {
            command.Enabled = !IsRunning;
        }

        Task ICommandHandler<RunProcessCommandDefenition>.Run(Command command)
        {
            var startElement = _elements.FirstOrDefault(e => e.Element is StartPoint);
            if (startElement == null)
                return TaskUtility.Completed;

            var startPoint = startElement.Element as StartPoint;
            if (startPoint != null)
            {
                _thread = new Thread(startPoint.Run);
                _thread.Start();
                _log.Info("Thread started");
                NotifyOfPropertyChange(() => IsRunning);
            }
            return TaskUtility.Completed;
        }

        void ICommandHandler<StopProcessCommandDefenition>.Update(Command command)
        {
            command.Enabled = IsRunning;
        }

        Task ICommandHandler<StopProcessCommandDefenition>.Run(Command command)
        {
            if (IsRunning)
            {
                _log.Info("Interrupting running thread");
                _thread.Interrupt();
                NotifyOfPropertyChange(() => IsRunning);
                return Task.Delay(2000).ContinueWith(delegate
                {
                    if (IsRunning)
                    {
                        _log.Info("Thread is still running. Aborting Thread");
                        _thread.Abort();
                        _thread = null;
                    }
                    NotifyOfPropertyChange(() => IsRunning);
                });
            }

            return TaskUtility.Completed;
        }
    }
}
