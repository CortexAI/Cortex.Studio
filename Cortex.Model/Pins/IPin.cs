using System;

namespace Cortex.Model.Pins
{
   public interface IPin
    {
        string Name { get; }
    }

    public interface IInputPin : IPin
    {
        void Attach(IOutputPin pin);
        void Detach(IOutputPin pin);
    }

    public interface IOutputPin : IPin
    {
    }

    public interface IDataPin : IPin
    {
        Object Value { get; }

        Type Type { get; }
    }
    
    public interface IDataOutputPin : IDataPin, IOutputPin
    {
    }

    public interface IDataInputPin : IDataPin, IInputPin
    {
    }

    public interface IFlowInputPin : IInputPin
    {
    }

    public interface IFlowOutputPin : IOutputPin
    {
        event Action<Flow> Called;
        void Call(Flow flow);
    }
}
