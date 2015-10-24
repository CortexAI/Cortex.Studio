using System;
using System.Windows;
using System.Windows.Media;

namespace Cortex.Studio.Modules.ProcessDesigner.ViewModels
{
    public interface IConnectorViewModel
    {
        event EventHandler PositionChanged;
        ElementViewModel Element { get; }
        string Name { get; }
        Color Color { get; }
        Type Type { get; }
        Point Position { get; }
        bool IsConnected { get; }
        ConnectorDirection ConnectorDirection { get; }

        void Attach(ConnectionViewModel connection);
        void Detach(ConnectionViewModel connection);
    }
}