using Cortex.Core.Model.Pins;

namespace Cortex.Core.Model
{
    public interface IConnection : INode
    {
        IElement StartElement { get; }
        IElement EndElement { get; }
        IOutputPin StartPin { get; }
        IInputPin EndPin { get; }
    }
}