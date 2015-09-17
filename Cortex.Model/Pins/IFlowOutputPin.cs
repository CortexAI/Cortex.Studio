using System;

namespace Cortex.Model.Pins
{
    public interface IFlowOutputPin : IOutputPin
    {
        event Action<Flow> Called;
        void Call(Flow flow);
    }
}