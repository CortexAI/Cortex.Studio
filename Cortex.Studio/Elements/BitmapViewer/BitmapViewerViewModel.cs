using System;
using System.Windows.Media.Imaging;
using Cortex.Model;

namespace Cortex.Studio.Elements.BitmapViewer
{
    class BitmapViewerViewModel : EditorViewModel
    {
        private readonly BitmapViewer _bitmapViewer;

        private BitmapSource _image;

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

        public BitmapViewerViewModel(IElement element) : base(element)
        {
            _bitmapViewer = element as BitmapViewer;
        }

        public override void AfterElementCalled(Flow flow)
        {
            Image = _bitmapViewer.Bitmap;
            base.AfterElementCalled(flow);
        }
    }
}
