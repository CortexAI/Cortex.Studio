using System.Collections.Generic;
using Cortex.Model.Pins;

namespace Cortex.Model
{
    public interface IElement
    {
        IEnumerable<IInputPin> Inputs { get; }
        IEnumerable<IOutputPin> Outputs { get; }
    }
}