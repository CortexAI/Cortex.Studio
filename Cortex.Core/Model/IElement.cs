using System.Collections.Generic;
using Cortex.Core.Model.Pins;

namespace Cortex.Core.Model
{
    public interface IElement : INode
    {
        IEnumerable<IInputPin> Inputs { get; }
        IEnumerable<IOutputPin> Outputs { get; }
    }
}