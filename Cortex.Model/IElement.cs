using System;
using Cortex.Model.Pins;

namespace Cortex.Model
{
    public interface IElement
    {
        IInputPin[] Inputs { get; }
        IOutputPin[] Outputs { get; }
    }
}