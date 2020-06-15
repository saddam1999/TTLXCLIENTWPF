using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x0200000E RID: 14
	public class ImageHelper
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002714 File Offset: 0x00000914
		public static byte[] BitmapImage2Bytes(BitmapImage bmp)
		{
			byte[] data = null;
			Stream bmpStream = bmp.StreamSource;
			if (bmpStream != null && bmpStream.Length > 0L)
			{
				bmpStream.Position = 0L;
				using (BinaryReader br = new BinaryReader(bmpStream))
				{
					data = br.ReadBytes((int)bmpStream.Length);
				}
			}
			return data;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002774 File Offset: 0x00000974
		public static BitmapImage Bytes2BitmapImage(byte[] bytes)
		{
			BitmapImage result;
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.StreamSource = ms;
				bitmapImage.EndInit();
				bitmapImage.Freeze();
				result = bitmapImage;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000027CC File Offset: 0x000009CC
		public static byte[] Image2Bytes(Image image)
		{
			if (image == null)
			{
				return null;
			}
			byte[] data = null;
			using (MemoryStream ms = new MemoryStream())
			{
				using (Bitmap Bitmap = new Bitmap(image))
				{
					Bitmap.Save(ms, ImageFormat.Png);
					ms.Position = 0L;
					data = new byte[ms.Length];
					ms.Read(data, 0, Convert.ToInt32(ms.Length));
					ms.Flush();
				}
			}
			return data;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000285C File Offset: 0x00000A5C
		public static Bitmap Bytes2Image(byte[] bytes)
		{
			Bitmap result;
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				result = new Bitmap(ms);
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002894 File Offset: 0x00000A94
		public static Bitmap BitmapSource2Bitmap(BitmapSource bitmapSource)
		{
			Bitmap bmp = new Bitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, PixelFormat.Format32bppPArgb);
			BitmapData data = bmp.LockBits(new Rectangle(System.Drawing.Point.Empty, bmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
			bitmapSource.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
			bmp.UnlockBits(data);
			return bmp;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002904 File Offset: 0x00000B04
		public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
		{
			Bitmap result;
			using (MemoryStream outStream = new MemoryStream())
			{
				new BmpBitmapEncoder
				{
					Frames = 
					{
						BitmapFrame.Create(bitmapImage)
					}
				}.Save(outStream);
				result = new Bitmap(outStream);
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002958 File Offset: 0x00000B58
		public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
		{
			BitmapImage result;
			using (MemoryStream stream = new MemoryStream())
			{
				bitmap.Save(stream, ImageFormat.Png);
				stream.Position = 0L;
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.StreamSource = stream;
				bitmapImage.EndInit();
				bitmapImage.Freeze();
				result = bitmapImage;
			}
			return result;
		}
	}
}
