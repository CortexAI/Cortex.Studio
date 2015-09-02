using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Cortex.Model.Elements;
using Cortex.Model.Elements.Logic;
using Cortex.Modules.Core;
using Cortex.Modules.ProcessDesigner.Commands;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using Gemini.Modules.Inspector;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    [Serializable]
    [Export(typeof(GraphViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GraphViewModel : FileDocument, ISerializable, 
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

        public GraphViewModel(SerializationInfo info, StreamingContext context) : base(null)
        {
            var count = info.GetInt32("ElementsCount");
            for (var i = 0; i < count; i++)
            {
                var e = (ElementViewModel)info.GetValue("Element_" + i, typeof(ElementViewModel));
                _elements.Add(e);
            }
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
                sourceConnector.Connections.Remove(newConnection);
                Connections.Remove(newConnection);
                _log.Warn("No target for connection was found or target is a source");
                return;
            }

            // Connection already exist
            if (Connections.FirstOrDefault(c => c.From == sourceConnector && c.To == nearbyConnector) != null)
            {
                sourceConnector.Connections.Remove(newConnection);
                Connections.Remove(newConnection);
                _log.Warn("That connection already exists");
                return;
            }

            try
            {
                if (!nearbyConnector.AllowMultipleConnections && nearbyConnector.IsConnected)
                {
                    Connections.RemoveRange(nearbyConnector.Connections);
                    nearbyConnector.DetachAll();
                    _log.Warn("Target connector doesn't support multiple inputs. Removing other connectors");
                }
                newConnection.To = nearbyConnector;
                _log.Info("Connection created");
            }
            catch (Exception ex)
            {
                sourceConnector.Connections.Remove(newConnection);
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
            Connections.RemoveRange(element.AttachedConnections);
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
            var formatter = new SoapFormatter();
            using (var stream = new FileStream(FileName, FileMode.Create))
            {
                formatter.Serialize(stream, this);
                stream.Close();
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ElementsCount", _elements.Count);
            for (var i = 0; i < _elements.Count; i++)
            {
                info.AddValue("Element_" + i, _elements[i]);
            }
        }

        [OnDeserialized]
        void OnDeserialize(StreamingContext context)
        {
            foreach (var elementViewModel in _elements)
            {
                if (elementViewModel.Element == null || elementViewModel.Element.Inputs == null) 
                    continue;
                foreach (var inputPin in elementViewModel.Element.Inputs)
                {
                    if(!inputPin.IsConnected)
                        continue;
                    var to = elementViewModel.InputConnectors.FirstOrDefault(c => c.Pin.Equals(inputPin));

                    foreach (var outputPin in inputPin.Connected)
                    {
                        var sourceElement =
                            _elements.FirstOrDefault(element => element.Element.Outputs.Contains(outputPin));
                        if (sourceElement == null)
                            throw new Exception("Missed source element");
                        var from = sourceElement.OutputConnectors.FirstOrDefault(c => c.Pin.Equals(outputPin));
                        _connections.Add(new ConnectionViewModel(@from, to));
                    }
                }
            }
        }
    }
}
