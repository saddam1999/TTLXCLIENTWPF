namespace TTLX.WindowsTool.Common.Utility
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows;
    using System.Windows.Media.Imaging;

    public class ImageHelper
    {
        public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0L;
                BitmapImage image1 = new BitmapImage();
                image1.BeginInit();
                image1.CacheOption = BitmapCacheOption.OnLoad;
                image1.StreamSource = stream;
                image1.EndInit();
                image1.Freeze();
                return image1;
            }
        }

        public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BmpBitmapEncoder { Frames = { BitmapFrame.Create((BitmapSource) bitmapImage) } }.Save(stream);
                return new Bitmap(stream);
            }
        }

        public static byte[] BitmapImage2Bytes(BitmapImage bmp)
        {
            byte[] buffer = null;
            Stream streamSource = bmp.StreamSource;
            if ((streamSource == null) || (streamSource.Length <= 0L))
            {
                return buffer;
            }
            streamSource.Position = 0L;
            using (BinaryReader reader = new BinaryReader(streamSource))
            {
                return reader.ReadBytes((int) streamSource.Length);
            }
        }

        public static Bitmap BitmapSource2Bitmap(BitmapSource bitmapSource)
        {
            Bitmap bitmap = new Bitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, PixelFormat.Format32bppPArgb);
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            bitmapSource.CopyPixels(Int32Rect.get_Empty(), bitmapdata.Scan0, bitmapdata.Height * bitmapdata.Stride, bitmapdata.Stride);
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        public static BitmapImage Bytes2BitmapImage(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BitmapImage image1 = new BitmapImage();
                image1.BeginInit();
                image1.CacheOption = BitmapCacheOption.OnLoad;
                image1.StreamSource = stream;
                image1.EndInit();
                image1.Freeze();
                return image1;
            }
        }

        public static Bitmap Bytes2Image(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new Bitmap(stream);
            }
        }

        public static byte[] Image2Bytes(Image image)
        {
            if (image == null)
            {
                return null;
            }
            byte[] buffer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(image))
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    stream.Position = 0L;
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                    stream.Flush();
                }
            }
            return buffer;
        }
    }
}

