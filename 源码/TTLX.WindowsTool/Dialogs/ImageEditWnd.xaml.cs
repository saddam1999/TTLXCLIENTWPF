using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x02000086 RID: 134
	public partial class ImageEditWnd : MetroWindow
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000629 RID: 1577 RVA: 0x0001E078 File Offset: 0x0001C278
		// (remove) Token: 0x0600062A RID: 1578 RVA: 0x0001E0B0 File Offset: 0x0001C2B0
		private event Action<string> SaveAction;

		// Token: 0x0600062B RID: 1579 RVA: 0x0001E0E5 File Offset: 0x0001C2E5
		public ImageEditWnd(Action<string> onSaved, int ratioWidth = 16, int ratioHeight = 9, int maxSize = 1920)
		{
			this.InitializeComponent();
			this.SaveAction = onSaved;
			this._maxSize = maxSize;
			this._ratioWidth = ratioWidth;
			this._ratioHeight = ratioHeight;
			base.Loaded += delegate(object sender, RoutedEventArgs args)
			{
				this.LoadFile();
			};
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001E124 File Offset: 0x0001C324
		private System.Drawing.Size GetProperSize(int w, int h, int maxSize = 0)
		{
			if (maxSize == 0)
			{
				maxSize = this._maxSize;
			}
			double num = (double)w;
			double num2 = (double)h;
			if (num2 > num)
			{
				if (num2 > (double)maxSize)
				{
					num = num * (double)maxSize / num2;
					num2 = (double)maxSize;
				}
			}
			else if (num > (double)maxSize)
			{
				num2 = num2 * (double)maxSize / num;
				num = (double)maxSize;
			}
			return new System.Drawing.Size((int)num, (int)num2);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001E170 File Offset: 0x0001C370
		private void XBtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				string tempJpgPath = Helper.GetTempJpgPath();
				ISupportedImageFormat format = new JpegFormat
				{
					Quality = 90
				};
				using (ImageFactory imageFactory = new ImageFactory(false))
				{
					System.Drawing.Image image = imageFactory.Load(this.XImageEdit.CroppedImage).Image;
					System.Drawing.Size properSize = this.GetProperSize(image.Width, image.Height, 0);
					imageFactory.Resize(new ResizeLayer(properSize, ImageProcessor.Imaging.ResizeMode.Stretch, AnchorPosition.Center, true, null, null, null, null)).Format(format).Save(tempJpgPath);
				}
				Action<string> saveAction = this.SaveAction;
				if (saveAction != null)
				{
					saveAction(tempJpgPath);
				}
				base.Close();
			}
			catch (Exception pException)
			{
				LogHelper.Error("ImageEditWnd->XBtnSave_OnClick", pException);
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001E24C File Offset: 0x0001C44C
		private void XBtnSaveOrigin_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				string tempJpgPath = Helper.GetTempJpgPath();
				ISupportedImageFormat format = new JpegFormat
				{
					Quality = 90
				};
				using (ImageFactory imageFactory = new ImageFactory(false))
				{
					System.Drawing.Image image = imageFactory.Load(this.XImageEdit.OriginImage).Image;
					System.Drawing.Size properSize = this.GetProperSize(image.Width, image.Height, 0);
					imageFactory.Resize(new ResizeLayer(properSize, ImageProcessor.Imaging.ResizeMode.Stretch, AnchorPosition.Center, true, null, null, null, null)).Format(format).Save(tempJpgPath);
				}
				Action<string> saveAction = this.SaveAction;
				if (saveAction != null)
				{
					saveAction(tempJpgPath);
				}
				base.Close();
			}
			catch (Exception pException)
			{
				LogHelper.Error("ImageEditWnd->XBtnSaveOrigin_OnClick", pException);
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001E328 File Offset: 0x0001C528
		private void XBtnSetRatio_OnClick(object sender, RoutedEventArgs e)
		{
			string text = ((Button)sender).Tag.ToString();
			double width = double.Parse(text.Split(new char[]
			{
				':'
			})[0]);
			double height = double.Parse(text.Split(new char[]
			{
				':'
			})[1]);
			this.XImageEdit.CroppedImageByRatio(width, height);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001E388 File Offset: 0x0001C588
		private void XBtnCustomRatio_OnClick(object sender, RoutedEventArgs e)
		{
			double width;
			double height;
			if (double.TryParse(this.XTxtWidth.Text, out width) && double.TryParse(this.XTxtHeight.Text, out height))
			{
				this.XImageEdit.CroppedImageByRatio(width, height);
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001E3CA File Offset: 0x0001C5CA
		private void XBtnRotate_OnClick(object sender, RoutedEventArgs e)
		{
			this.XImageEdit.RotateImage(int.Parse(((Button)sender).Tag.ToString()));
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001E3EC File Offset: 0x0001C5EC
		private void XMediaSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.XMedia.Position = TimeSpan.FromMilliseconds(this.XMediaSlider.Value);
			int pixelWidth = (int)this.XMedia.RenderSize.Width;
			int pixelHeight = (int)this.XMedia.RenderSize.Height;
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(pixelWidth, pixelHeight, 96.0, 96.0, PixelFormats.Pbgra32);
			renderTargetBitmap.Render(this.XMedia);
			this.XImageEdit.Init(renderTargetBitmap);
			this.XImageEdit.CroppedImageByRatio((double)this._ratioWidth, (double)this._ratioHeight);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001E48C File Offset: 0x0001C68C
		private void XMedia_OnMediaInited()
		{
			this.XMedia.UpdateLayout();
			int pixelWidth = (int)this.XMedia.RenderSize.Width;
			int pixelHeight = (int)this.XMedia.RenderSize.Height;
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(pixelWidth, pixelHeight, 96.0, 96.0, PixelFormats.Pbgra32);
			renderTargetBitmap.Render(this.XMedia);
			this.XImageEdit.Init(renderTargetBitmap);
			this.XImageEdit.CroppedImageByRatio((double)this._ratioWidth, (double)this._ratioHeight);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001E51C File Offset: 0x0001C71C
		private void LoadFile()
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "选择图片或者视频";
				openFileDialog.Filter = "图片或者视频文件|*.jpg;*.jpeg;*.png;*.bmp;*.mp4;*.wmv;*.avi;*.mkv";
				if (openFileDialog.ShowDialog() == true)
				{
					string fileName = openFileDialog.FileName;
					string extension = Path.GetExtension(fileName);
					if (extension.Equals(".mp4") || extension.Equals(".wmv") || extension.Equals(".avi") || extension.Equals(".mkv"))
					{
						this.XMedia.Source = new Uri(fileName);
						this.XMediaSlider.Visibility = Visibility.Visible;
					}
					else
					{
						string tempJpgPath = Helper.GetTempJpgPath();
						ISupportedImageFormat format = new JpegFormat
						{
							Quality = 90
						};
						using (ImageFactory imageFactory = new ImageFactory(false))
						{
							System.Drawing.Image image = imageFactory.Load(fileName).Image;
							System.Drawing.Size properSize = this.GetProperSize(image.Width, image.Height, 0);
							imageFactory.Resize(new ResizeLayer(properSize, ImageProcessor.Imaging.ResizeMode.Stretch, AnchorPosition.Center, true, null, null, null, null)).Format(format).Save(tempJpgPath);
						}
						this.XImageEdit.Init(tempJpgPath);
						this.XImageEdit.CroppedImageByRatio((double)this._ratioWidth, (double)this._ratioHeight);
						this.XMediaSlider.Visibility = Visibility.Hidden;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		private void XBtnOpen_OnClick(object sender, RoutedEventArgs e)
		{
			this.LoadFile();
		}

		// Token: 0x040002E4 RID: 740
		private readonly int _maxSize;

		// Token: 0x040002E5 RID: 741
		private readonly int _ratioWidth;

		// Token: 0x040002E6 RID: 742
		private readonly int _ratioHeight;
	}
}
