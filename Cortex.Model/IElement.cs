using System;
using Cortex.Model.Pins;

namespace Cortex.Model
{
    public interface IElement
    {
        string Name { get; }
        Uri IconUri { get; }
        string Description { get; }
        string Category { get; }
        IInputPin[] Inputs { get; }
        IOutputPin[] Outputs { get; }
    }
}