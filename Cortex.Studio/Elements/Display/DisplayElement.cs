using Cortex.Core.Model;

namespace Cortex.Studio.Elements.Display
{
    class DisplayElement : Node
    {
        public DisplayElement()
        {
            var input = new InputPin<object>("Input");
            AddInputPin(input);
        }

        protected override void Handler()
        {
        }
    }
}
