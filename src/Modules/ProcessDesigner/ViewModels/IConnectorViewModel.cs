using System;
using System.Windows;
using System.Windows.Media;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public interface IConnectorViewModel
    {
        event EventHandler PositionChanged;
        ElementViewModel Element { get; }
        string Name { get; }
        Color Color { get; }
        Point Position { get; }
        bool IsConnected { get; }
        ConnectorDirection ConnectorDirection { get; }
    }
}