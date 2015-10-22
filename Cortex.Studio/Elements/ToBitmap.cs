using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cortex.Model;
using Cortex.Model.Elements;
using Cortex.Model.Pins;
using Cortex.Model.Serialization;

namespace Cortex.Studio.Elements
{
    class ToBitmap : BaseElement
    {
        private readonly FlowOutputPin _flowOut;
        private readonly DataInputPin _dataPin;
        private readonly DataOutputPin _bitmapPin;
        private WriteableBitmap _bitmap;
        private Int32Rect _rect;
        
        private int _bytesPerPixel;
        private int _width = 640;
        private int _height = 480;
        private PixelFormat _format = PixelFormats.Rgb24;

        public int Height
        {
            get { return _height; }
            set
            {
                _height = value; 
                Rebuild();
            }
        }

        public int Width
        {
            get { return _width; }
            set { 
                _width = value;
                Rebuild(); 
            }
        }

        public int BytesPerPixel
        {
            get { return _bytesPerPixel; }
            set
            {
                _bytesPerPixel = value; 
                Rebuild();
            }
        }

        public ToBitmap()
        {
            _dataPin = new DataInputPin("Data", typeof(byte[]));
            _flowOut = new FlowOutputPin();
            _bitmapPin = new DataOutputPin("Bitmap", typeof(WriteableBitmap));
            
            _width = 640;
            _height = 480;
            _bytesPerPixel = 4;
            
            Rebuild();

            AddInputPin(new FlowInputPin(OnCall));
            AddInputPin(_dataPin);
            AddOutputPin(_flowOut);
            AddOutputPin(_bitmapPin);
        }

        private void Rebuild()
        {
            if (BytesPerPixel == 4)
                _format = PixelFormats.Bgr32;
            else if (BytesPerPixel == 3)
                _format = PixelFormats.Rgb24;
            

            _bitmap = new WriteableBitmap(Width, Height, 96, 96, _format, null);
            _rect = new Int32Rect(0, 0, Width, Height);
            _bitmapPin.Value = _bitmap;
        }

        private void OnCall(Flow flow)
        {
            try
            {
                _bitmap.WritePixels(_rect, (byte[])_dataPin.Value, (int)_bitmap.Width * ((_format.BitsPerPixel + 7) / 8), 0);
                _flowOut.Call(flow);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to write a bitmap");
            }
        }

        public override void Load(IPersisterReader reader)
        {
            _width = reader.Get<int>("Width");
            _height = reader.Get<int>("Height");
            _bytesPerPixel = reader.Get<int>("BPP");
            Rebuild();
            base.Load(reader);
        }

        public override void Save(IPersisterWriter writer)
        {
            writer.Set("Width", _width);
            writer.Set("Height", _height);
            writer.Set("BPP", _bytesPerPixel);
            base.Save(writer);
        }
    }
}
