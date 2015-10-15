using Cortex.Model.Pins;

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