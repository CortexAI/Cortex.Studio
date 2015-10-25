using System;

namespace Cortex.Core.Model.Pins
{
    public interface IFlowOutputPin : IOutputPin
    {
        event Action<Flow> Called;
        void Call(Flow flow);
    }
}