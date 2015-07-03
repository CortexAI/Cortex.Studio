using System;
using System.Collections.Generic;

namespace Cortex.Model.Pins
{
    public interface IPin
    {
        string Name { get; }
        Type Type { get; }
        Object Value { get; }
    }

    public interface IOutputPin : IPin
    {
        Object Value { get; set; }
    }

    public interface IInputPin : IPin
    {
        IEnumerable<IOutputPin> Connected { get; }
        bool IsConnected { get; }
        bool AllowMultipleConnections { get; }
        void Attach(IOutputPin pin);
        void Detach(IOutputPin pin);
        void DetachAll();
    }
}
