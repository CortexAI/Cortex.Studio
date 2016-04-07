using System.Windows.Media.Imaging;
using Cortex.Core.Model;

namespace Cortex.Studio.Elements.BitmapViewer
{
    class BitmapViewerViewModel : EditorViewModel
    {
        private BitmapSource _image;

        public override string Name => "Bitmap viewer";
        public BitmapSource Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        public BitmapViewerViewModel(INode element) : base(element)
        {
        }

        public override void AfterInputPinProcessed(IInputPin input, object o)
        {
            Image = o as WriteableBitmap;
            base.AfterInputPinProcessed(input, o);
        }
    }
}
