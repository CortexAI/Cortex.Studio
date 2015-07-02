using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cortex.Modules.ProjectExplorer.Util
{
    /// <summary>
    /// Reference: http://www.geekpedia.com/tutorial219_Extracting-Icons-from-Files.html
    /// </summary>
    public static class FileIcons
    {
        // Constants that we need in the function call
        private const int SHGFI_ICON = 0x100;
        private const int SHGFI_SMALLICON = 0x1;
        private const int SHGFI_LARGEICON = 0x0;

        private struct ShFileInfo
        {
            // Handle to the icon representing the file
            public IntPtr hIcon;
            // Index of the icon within the image list
            public int iIcon;
            // Various attributes of the file
            public uint dwAttributes;
            // Path to the file
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string szDisplayName;
            // File type
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
        };

        // The signature of SHGetFileInfo (located in Shell32.dll)
        [DllImport("Shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref ShFileInfo psfi,
            int cbFileInfo, uint uFlags);

        public static Bitmap GetSmallIcon(string fileName)
        {
            var shinfo = new ShFileInfo();
            SHGetFileInfo(fileName, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
            var myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
            return myIcon.ToBitmap();
        }

        public static Bitmap GetLargeIcon(string fileName)
        {
            var shinfo = new ShFileInfo();
            SHGetFileInfo(fileName, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
            var myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
            return myIcon.ToBitmap();
        }

        public static ImageSource GetImageSource(Bitmap bmp)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                      bmp.GetHbitmap(),
                      IntPtr.Zero,
                      Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
