namespace Cortex.Model.Pins
{
    public interface IInputPin : IPin
    {
        void Attach(IOutputPin pin);
        void Detach(IOutputPin pin);
    }
}