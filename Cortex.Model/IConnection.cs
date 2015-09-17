using Cortex.Model.Pins;
using Cortex.Model.Serialization;

namespace Cortex.Model
{
    public interface IConnection : INode
    {
        IElement StartElement { get; }
        IElement EndElement { get; }
        IOutputPin StartPin { get; }
        IInputPin EndPin { get; }
    }
}