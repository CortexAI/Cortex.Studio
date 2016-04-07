using System.Windows.Media.Imaging;
using Cortex.Core.Model;

namespace Cortex.Studio.Elements.BitmapViewer
{
    class BitmapViewer : Node
    {
        private readonly InputPin<object> _input;

        public BitmapViewer()
        {
            _input = new InputPin<object>("Input");
        }

        protected override void Handler()
        {
        }
    }
}
