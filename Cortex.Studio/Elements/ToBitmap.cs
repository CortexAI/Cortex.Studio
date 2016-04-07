using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cortex.Core.Nodes;
using Cortex.Core.Model;
using Cortex.Core.Model.Serialization;

namespace Cortex.Studio.Elements
{
    class ToBitmap : Node
    {
        private readonly InputPin<byte[]> _input;
        private readonly OutputPin<WriteableBitmap> _bitmapPin;
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
            _bitmapPin = new OutputPin<WriteableBitmap>("Bitmap");
            _input = new InputPin<byte[]>("Data");

            _width = 640;
            _height = 480;
            _bytesPerPixel = 4;
            
            Rebuild();

            
            AddInputPin(_input);
            AddOutputPin(_bitmapPin);
        }

        private void Rebuild()
        {
            if (BytesPerPixel == 4)
                _format = PixelFormats.Bgr32;
            else if (BytesPerPixel == 3)
                _format = PixelFormats.Rgb24;
            else if (BytesPerPixel == 2)
                _format = PixelFormats.Gray16;
            

            _bitmap = new WriteableBitmap(Width, Height, 96, 96, _format, null);
            _rect = new Int32Rect(0, 0, Width, Height);
        }

        public override void Load(IPersisterReader reader)
        {
            _width = reader.Get<int>("Width");
            _height = reader.Get<int>("Height");
            _bytesPerPixel = reader.Get<int>("BPP");
            Rebuild();
            base.Load(reader);
        }

        protected override void Handler()
        {
            try
            {
                _bitmap.WritePixels(_rect, _input.Take(), (int)_bitmap.Width * ((_format.BitsPerPixel + 7) / 8), 0);
                _bitmapPin.Emit(_bitmap);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to write a bitmap");
            }
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
