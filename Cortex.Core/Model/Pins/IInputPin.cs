namespace Cortex.Core.Model.Pins
{
    public interface IInputPin : IPin
    {
        void Attach(IOutputPin pin);
        void Detach(IOutputPin pin);
    }
}