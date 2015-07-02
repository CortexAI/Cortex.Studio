using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Elements;
using Cortex.Modules.Core;
using Cortex.Modules.ProcessDesigner.Commands;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using Gemini.Modules.Inspector;
using MahApps.Metro.Controls;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    [Serializable]
    [Export(typeof(GraphViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GraphViewModel : FileDocument, ISerializable, ICommandHandler<RunProcessCommandDefenition>
    {
        private readonly IInspectorTool _inspectorTool = IoC.Get<IInspectorTool>();
        private readonly ILog _log = LogManager.GetLog(typeof (GraphViewModel));
        
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

            if (nearbyConnector == null || sourceConnector.Element == nearbyConnector.Element)
            {
                Connections.Remove(newConnection);
                return;
            }

            var existingConnection = nearbyConnector.Connection;
            
            try
            {
                newConnection.To = nearbyConnector;
                if (existingConnection != null)
                    Connections.Remove(existingConnection);
            }
            catch (Exception)
            {
                Connections.Remove(newConnection);
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

        public void Run()
        {
            var startElement = _elements.FirstOrDefault(e => e.Element is StartPoint);
            if (startElement == null) return;

            var startPoint = startElement.Element as StartPoint;
            if (startPoint != null)
            {
                startPoint.Run();
            }
        }
        
        public override void Save()
        {
            var formatter = new SoapFormatter();
            using (var stream = new FileStream(FileName, FileMode.Create))
            {
                formatter.Serialize(stream, this);
                stream.Close();
            }
        }

        void ICommandHandler<RunProcessCommandDefenition>.Update(Command command)
        {
            // TODO: update run command availability
            // command.Enabled = 
        }

        Task ICommandHandler<RunProcessCommandDefenition>.Run(Command command)
        {
            this.Run();
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
            foreach (var e in _elements.Where(element => element.Element.Inputs != null))
            {
                foreach (var input in e.Element.Inputs.Where(i => i.ConnectedPin != null))
                {
                    var to = e.InputConnectors.FirstOrDefault(c => c.Pin.Equals(input));
                    var sourceElement = _elements.FirstOrDefault(element => element.Element.Outputs.Contains(input.ConnectedPin));

                    if (sourceElement == null)
                        throw new Exception("Missed source element");

                    var from = sourceElement.OutputConnectors.FirstOrDefault(c => c.Pin.Equals(input.ConnectedPin));
                    _connections.Add(new ConnectionViewModel(from, to));
                }
            }
        }
    }
}
