using System.Collections.Generic;
using Cortex.Model.Pins;

namespace Cortex.Model
{
    public interface IElement : INode
    {
        IEnumerable<IInputPin> Inputs { get; }
        IEnumerable<IOutputPin> Outputs { get; }
    }
}